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

                              Mapper.Initialize(cfg => cfg.CreateMap<eUseControl.Models.UserSignUp, eUseControl.Domain.Entities.User.UserSignUp>());

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

          [Admin]
          public ActionResult CreatePlaylist(Playlists playlist, int[] musicIds)
          {
               if (ModelState.IsValid)
               {
                    if (musicIds != null && musicIds.Length > 0)
                    {
                         // Populate the playlist object with the submitted data
                         playlist.Musics = _musiccontext.Musics.Where(m => musicIds.Contains(m.MusicId)).ToList();

                         _musiccontext.Playlists.Add(playlist);
                         _musiccontext.SaveChanges();

                         return RedirectToAction("CreatePlaylist");
                    }
                    else
                    {
                         ModelState.AddModelError("", "Please select at least one music.");
                    }
               }

               ViewBag.Genres = _musiccontext.Genres.ToList();
               ViewBag.Musics = _musiccontext.Musics.ToList();
               return View(playlist);
          }


          public ActionResult Playlist()
          {
               ViewBag.Genres = _musiccontext.Genres.ToList();
               ViewBag.Musics = _musiccontext.Musics.ToList(); // Fetch all available music
               var playlists = _musiccontext.Playlists.Include(p => p.Musics).ToList(); // Include Musics for each Playlist
               return View(playlists); // Pass the list of playlists to the view
          }


          public ActionResult PlaylistDetails(int id)
          {
               var playlist = _musiccontext.Playlists
                                           .Include(p => p.Musics.Select(m => m.UserSignUp)) // Include associated artists
                                           .FirstOrDefault(p => p.PlaylistId == id);

               if (playlist == null)
               {
                    return HttpNotFound();
               }


               var musicList = playlist.Musics.Select(m =>
               {
                    var artist = _context.Users.FirstOrDefault(u => u.UserId == m.UserSignUpId);
                    m.UserSignUp = artist;
                    return m;
               }).ToList();

               ViewBag.MusicList = musicList;

               return View("PlaylistDetails", playlist); // Pass the playlist to the view
          }

          public ActionResult NewMusic()
          {
               var newestMusics = _musiccontext.Musics
                   .Include(m => m.Genres)
                   .Include(m => m.UserSignUp)
                   .OrderByDescending(m => m.Created)
                   .ToList();

               var musicList = newestMusics.Select(m =>
               {
                    var artist = _context.Users.FirstOrDefault(u => u.UserId == m.UserSignUpId);
                    m.UserSignUp = artist;
                    return m;
               }).ToList();

               ViewBag.MusicList = musicList;
               return View();
          }

          public ActionResult MostPlayed()
          {
               // Get the newest musics with related genres and users
               var newestMusics = _musiccontext.Musics
                   .Include(m => m.Genres)
                   .Include(m => m.UserSignUp)
                   .OrderByDescending(m => m.Created)
                   .ToList();

               var musicList = newestMusics.Select(m =>
               {
                    var artist = _context.Users.FirstOrDefault(u => u.UserId == m.UserSignUpId);
                    m.UserSignUp = artist;
                    return m;
               }).ToList();

               ViewBag.MusicList = musicList;

               // Fetch most played music for each period
               var mostPlayedDaily = _musiccontext.Musics
                   .OrderByDescending(m => m.DailyListenCount)
                   .Include(m => m.Genres)
                   .Include(m => m.UserSignUp)
                   .Take(10)
                   .ToList();

               var mostPlayedWeekly = _musiccontext.Musics
                   .OrderByDescending(m => m.WeeklyListenCount)
                   .Include(m => m.Genres)
                   .Include(m => m.UserSignUp)
                   .Take(10)
                   .ToList();

               var mostPlayedMonthly = _musiccontext.Musics
                   .OrderByDescending(m => m.MonthlyListenCount)
                   .Include(m => m.Genres)
                   .Include(m => m.UserSignUp)
                   .Take(10)
                   .ToList();

               // Create a ViewModel to pass all the data
               var viewModel = new MostPlayedViewModel
               {
                    Daily = mostPlayedDaily,
                    Weekly = mostPlayedWeekly,
                    Monthly = mostPlayedMonthly
               };

               return View(viewModel);
          }

          public void IncrementListenCount(int musicId)
          {
               var music = _musiccontext.Musics.Find(musicId); // Use 'this' to refer to the current context
               if (music != null)
               {
                    var now = DateTime.Now;

                    // Reset counts if needed
                    if ((now - music.LastPlayedTime).TotalDays >= 1)
                    {
                         music.DailyListenCount = 0;
                    }
                    if ((now - music.LastPlayedTime).TotalDays >= 7)
                    {
                         music.WeeklyListenCount = 0;
                    }
                    if ((now - music.LastPlayedTime).TotalDays >= 30)
                    {
                         music.MonthlyListenCount = 0;
                    }

                    // Increment counts
                    music.DailyListenCount++;
                    music.WeeklyListenCount++;
                    music.MonthlyListenCount++;

                    // Update last played time
                    music.LastPlayedTime = now;

                    _musiccontext.SaveChanges();
               }
          }

          public ActionResult ArtistProfile()
          {
               // Get the username from the session
               string username = (string)System.Web.HttpContext.Current.Session["UserName"];

               // Fetch the user from the UserContext based on the username
               var user = _context.Users.FirstOrDefault(u => u.UserName == username);

               if (user != null && string.IsNullOrEmpty(user.ArtistName))
               {
                    // Artist name is null or empty, redirect to CreateArtistProfile
                    return RedirectToAction("CreateArtistProfile", "Home");
               }

               // Fetch the music list for the current user based on their ArtistName
               var musicList = _musiccontext.Musics
                   .Include(m => m.Genres)
                   .Include(m => m.UserSignUp)
                   .Where(m => m.UserSignUp.UserName == username) // Filter by the current user's username
                   .OrderByDescending(m => m.Created)
                   .ToList();

               return View(musicList);
          }


          public ActionResult CreateArtistProfile()
          {
               if (System.Web.HttpContext.Current.Session["UserName"] != null)
               {
                    return View();
               }

               return RedirectToAction("Login", "Login");
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CreateArtistProfile(UserSignUp model)
          {
               if (ModelState.IsValid)
               {
                    string username = (string)System.Web.HttpContext.Current.Session["UserName"];

                    var user = _context.Users.FirstOrDefault(u => u.UserName == username);
                    if (user != null)
                    {
                         user.ArtistName = model.ArtistName;
                         user.Role = URole.Artist;
                         _context.SaveChanges();
                    }

                    return RedirectToAction("ArtistProfile", "Home");
               }

               return View(model);
          }

          public ActionResult UserProfile()
          {
               return View();
          }

          public ActionResult AboutUs()
          {
               return View();
          }

          public ActionResult MusicSupport()
          {
               return View();
          }

          public ActionResult FAQ()
          {
               return View();
          }
     }
}