using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities;
using UserSignUp = eUseControl.Models.UserSignUp;
using AutoMapper;
using eUseControl.Extension;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic;
using System.Web.Management;
using eUseControl.Attributes;
using eUseControl.Domain.Entities.Music;
using System.Runtime.Remoting.Messaging;
using System.Data.Entity;


namespace eUseControl.Controllers
{
     public class HomeController : BaseController
     {
          private readonly UserContext _context;
          private readonly MusicContext _musiccontext;

          public HomeController()
          {
               _context = new UserContext();
               _musiccontext = new MusicContext();
          }
          public ActionResult Home()
          {
               SessionStatus();
               if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
               {
                    var user = System.Web.HttpContext.Current.GetMySessionObject();
                    UserData userData = new UserData
                    {
                         Username = user.Username,
                    };

                    // Fetch music data in random order and limit to 123 items
                    var musicList = _musiccontext.Musics
                                                 .Include("Genres")
                                                 .Include("UserSignUp")
                                                 .ToList()
                                                 .OrderBy(m => Guid.NewGuid())
                                                 .Take(8)
                                                 .ToList();

                    foreach (var music in musicList)
                    {
                         var artist = _context.Users.FirstOrDefault(u => u.UserId == music.UserSignUpId);
                         if (artist != null)
                         {
                              music.UserSignUp = artist;
                         }
                    }

                    ViewBag.MusicList = musicList;

                    return View(userData);
               }
               else
               {
                    // Fetch music data in random order and limit to 123 items
                    var musicList = _musiccontext.Musics
                                                 .Include("Genres")
                                                 .Include("UserSignUp")
                                                 .ToList()
                                                 .OrderBy(m => Guid.NewGuid())
                                                 .Take(8)
                                                 .ToList();

                    foreach (var music in musicList)
                    {
                         var artist = _context.Users.FirstOrDefault(u => u.UserId == music.UserSignUpId);
                         if (artist != null)
                         {
                              music.UserSignUp = artist;
                         }
                    }

                    ViewBag.MusicList = musicList;
                    return View();
               }
          }

          public ActionResult Search(string searchString)
          {
               using (var musicContext = new MusicContext())
               using (var userContext = new UserContext())
               {
                    var musicsQuery = from music in musicContext.Musics
                                      where music.Title.Contains(searchString) ||
                                            music.Genres.Any(g => g.Name.Contains(searchString)) ||
                                            music.UserSignUp.ArtistName.Contains(searchString)
                                      select music;

                    var musics = musicsQuery.Include(m => m.Genres)
                                            .Include(m => m.UserSignUp)
                                            .ToList();

                    // Fetch artists for each music
                    foreach (var music in musics)
                    {
                         var artist = userContext.Users.FirstOrDefault(u => u.UserId == music.UserSignUpId);
                         if (artist != null)
                         {
                              music.UserSignUp = artist;
                         }
                    }

                    var genres = musicContext.Genres.ToList();
                    if (genres != null)
                    {
                         ViewBag.AllGenres = genres;
                    }
                    else
                    {
                         ViewBag.AllGenres = new List<Genres>();
                    }

                    return View(musics);
               }
          }

          public ActionResult Genres()
          {
               var allGenres = _musiccontext.Genres.ToList(); // Fetch all genres from the database

               ViewBag.Genres = allGenres; // Pass all genres to the view
               return View();
          }
          public ActionResult MusicByGenre(int genreId)
          {
               var filteredMusic = _musiccontext.Musics
                                                .Include(m => m.Genres)
                                                .Include(m => m.UserSignUp)
                                                .Where(m => m.Genres.Any(g => g.GenreId == genreId))
                                                .ToList();

               ViewBag.GenreName = _musiccontext.Genres.FirstOrDefault(g => g.GenreId == genreId)?.Name;

               // Fetch music list without randomization and include ArtistName
               var musicList = filteredMusic.Select(m =>
               {
                    var artist = _context.Users.FirstOrDefault(u => u.UserId == m.UserSignUpId);
                    m.UserSignUp = artist;
                    return m;
               }).ToList();

               return View("MusicByGenre", musicList); // Pass the music list to the view
          }

          public ActionResult SignUp()
          {
               return View();
          }

          public ActionResult LogOut()
          {
               var apiCookie = Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    using (var db = new SessionContext())
                    {
                         var session = db.Sessions.FirstOrDefault(s => s.CookieString == apiCookie.Value);
                         if (session != null)
                         {
                              db.Sessions.Remove(session);
                              db.SaveChanges();
                         }
                    }
                    System.Web.HttpContext.Current.Session.Clear();
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";

                    apiCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(apiCookie);
               }
               return RedirectToAction("Home", "Home");
          }

          [ValidateAntiForgeryToken]
          [HttpPost]
          public ActionResult SignUp(UserSignUp model)
          {
               if (ModelState.IsValid)
               {
                    using (var userContext = new UserContext())
                    {
                         // Check if username or email already exists
                         bool userExists = userContext.Users.Any(u => u.UserName == model.UserName);
                         bool emailExists = userContext.Users.Any(u => u.EmailAddress == model.EmailAddress);

                         if (userExists)
                         {
                              ModelState.AddModelError("UserName", "Username already in use.");
                         }

                         if (emailExists)
                         {
                              ModelState.AddModelError("EmailAddress", "Email address already in use.");
                         }

                         if (!userExists && !emailExists)
                         {
                              string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                              if (string.IsNullOrEmpty(ipAddress))
                              {
                                   ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                              }

                              Mapper.Initialize(cfg => cfg.CreateMap<eUseControl.Models.UserSignUp, eUseControl.Domain.Entities.User.UserSignUp > ());

                              UserContext.CreateUser(model.FirstName, model.LastName, model.UserName, model.EmailAddress, model.Password, ipAddress);
                              return RedirectToAction("Home", "Home");
                         }
                    }
               }
               return View(model);
          }

          [Admin]
          public ActionResult AdminDashboard()
          {
               return View();
          }

          [Admin]
          public ActionResult Control()
          {
               IEnumerable<eUseControl.Domain.Entities.User.UserSignUp> users;
               using (var context = new UserContext())
               {
                    users = context.Users.ToList();
               }

               return View(users);

          }

          [Admin]
          [HttpPost]
          public ActionResult Modify(int userId, string firstName, string lastName, string userName, string emailAddress, string password, string ip)
          {
               var success = UserContext.ModifyUserData(userId, firstName, lastName, userName, emailAddress, password, ip);

               if (success)
               {
                    return RedirectToAction("Control", "Home");
               }
               else
               {
                    ViewBag.ErrorMessage = "Failed to modify user.";
                    var users = _context.LoadUsers();
                    return View("Control", users);
               }
          }

          [Admin]
          [HttpPost]
          public ActionResult Delete(int userId)
          {
               bool deletionSuccess = UserContext.DeleteUser(userId);

               if (deletionSuccess)
               {
                    return RedirectToAction("Control", "Home");
               }
               else
               {
                    ViewBag.ErrorMessage = "Failed to Delete user.";
                    var userContext = new UserContext();
                    var users = userContext.LoadUsers();
                    return View("Control", users);
               }

          }
    }
}