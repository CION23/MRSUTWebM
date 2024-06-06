using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using eUseControl.Attributes;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Music;
using eUseControl.Models;

namespace eUseControl.Web.Controllers
{
     public class MusicController : Controller
     {
          private readonly MusicContext _context;

          public MusicController()
          {
               _context = new MusicContext();
          }

          [Admin]
          public ActionResult ListMusic()
          {
               using (var userContext = new UserContext())
               using (var musicContext = new MusicContext())
               {
                    var musics = musicContext.Musics
                                            .Include(m => m.Genres)
                                            .Include(m => m.UserSignUp)
                                            .ToList();

                    foreach (var music in musics)
                    {
                         var user = userContext.Users.FirstOrDefault(u => u.UserId == music.UserSignUpId);
                         if (user != null)
                         {
                              music.UserSignUp = user; 
                         }
                    }

                    ViewBag.Genres = musicContext.Genres.ToList();
                    return View(musics);
               }
          }

          [Admin]
          [HttpPost]
          public ActionResult AddMusic(string title, int genresId, string artistName, string description, HttpPostedFileBase musicFile, HttpPostedFileBase imageFile)
          {
               if (musicFile != null && musicFile.ContentLength > 0 && imageFile != null && imageFile.ContentLength > 0)
               {
                    string musicFolderPath = Server.MapPath("~/Content/Music");
                    string imageFolderPath = Server.MapPath("~/Content/Image");

                    if (!Directory.Exists(musicFolderPath))
                    {
                         Directory.CreateDirectory(musicFolderPath);
                    }

                    if (!Directory.Exists(imageFolderPath))
                    {
                         Directory.CreateDirectory(imageFolderPath);
                    }

                    string musicName = Path.GetFileName(musicFile.FileName); // Get the file name
                    string musicNameFile = Path.Combine(musicFolderPath, musicName); // Combine with folder path

                    string imageName = Path.GetFileName(imageFile.FileName); // Get the file name
                    string imageNameFile = Path.Combine(imageFolderPath, imageName); // Combine with folder path

                    if (System.IO.File.Exists(musicNameFile))
                    {
                         ModelState.AddModelError("", "Music file already exists.");
                         return View(); // Return to the view with error
                    }

                    if (System.IO.File.Exists(imageNameFile))
                    {
                         ModelState.AddModelError("", "Image file already exists.");
                         return View(); // Return to the view with error
                    }

                    musicFile.SaveAs(musicNameFile);
                    imageFile.SaveAs(imageNameFile);

                    using (var musicContext = new MusicContext())
                    using (var userContext = new UserContext())
                    {
                         var artist = userContext.Users.FirstOrDefault(u => u.ArtistName == artistName);

                         if (artist != null)
                         {
                              // Modify the file paths to store only the relative paths in the database
                              string relativeMusicPath = "/Content/Music/" + musicName;
                              string relativeImagePath = "/Content/Image/" + imageName;

                              // Add new music record with relative file paths
                              musicContext.AddNewMusic(title, relativeMusicPath, relativeImagePath, genresId, artist.UserId, description);
                              musicContext.SaveChanges(); // Ensure the changes are saved

                              return RedirectToAction("ListMusic", "Music"); // Redirect to the ListMusic action
                         }
                         else
                         {
                              ModelState.AddModelError("", "Artist not found.");
                              return View(); // Return to the view with error
                         }
                    }
               }
               else
               {
                    ModelState.AddModelError("", "");
                    return View(); // Return to the view with error message
               }
          }

          [Admin]
          [HttpPost]
          public ActionResult ModifyMusic(int musicId, string title, string musicName, string imageName, int genreId, int artistId, int albumId, string description)
          {
               using (var context = new MusicContext())
               {
                    //context.ModifyMusic(musicId, title, musicName, imageName, genreId, artistId, albumId, description);
               }

               return RedirectToAction("Index", "Home");
          }

          [Admin]
          [HttpPost]
          public ActionResult DeleteMusic(int musicId)
          {
               using (var context = new MusicContext())
               {
                    //context.DeleteMusic(musicId);
               }

               return RedirectToAction("Index", "Home"); // Redirect to the home page or any other appropriate action
          }

     }
}
