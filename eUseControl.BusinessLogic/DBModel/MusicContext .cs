using eUseControl.Domain.Entities.Music;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using TagLib;

namespace eUseControl.BusinessLogic.DBModel
{
     public class MusicContext : DbContext
     {
          public MusicContext() : base("name=MVCMRSUTWEBDB")
          {
          }

          public DbSet<Domain.Entities.Music.Genres> Genres { get; set; }
          public DbSet<Musics> Musics { get; set; }
          public DbSet<Playlists> Playlists { get; set; }

          public List<Domain.Entities.Music.Genres> GetGenres()
          {
               using (var context = new MusicContext())
               {
                    return context.Genres.ToList();
               }
          }

          public void AddNewMusic(string title, string musicPath, string imagePath, int genresId, int artistId, string description)
          {
               using (var musicContext = new MusicContext())
               {
                    var genre = musicContext.Genres.FirstOrDefault(g => g.GenreId == genresId);

                    if (genre == null)
                    {
                         throw new InvalidOperationException("Genre not found");
                    }

                    TimeSpan calculatedDuration = musicContext.CalculateDuration(musicPath);

                    var newMusic = new Musics
                    {
                         Title = title,
                         FilePath = musicPath,
                         ImagePath = imagePath,
                         UserSignUpId = artistId,
                         Duration = calculatedDuration,
                         Created = DateTime.Now,
                         Updated = DateTime.Now,
                         Description = description,
                         LastPlayedTime = DateTime.Now,
                    };

                    newMusic.Genres = new List<Domain.Entities.Music.Genres> { genre };

                    musicContext.Musics.Add(newMusic);
                    musicContext.SaveChanges();
               }
          }

          public void ModifyMusic(int musicId, string title, string musicName, string imageName, int genreId, int artistId, int albumId, string description)
          {
               using (var context = new MusicContext())
               {
                    var existingMusic = context.Musics.Find(musicId);
                    if (existingMusic != null)
                    {
                         var filePath = $"~/Content/Musics/{musicName}";
                         var imagePath = $"~/Content/Images/{imageName}";

                         existingMusic.Title = title;
                         existingMusic.FilePath = filePath;
                         existingMusic.ImagePath = imagePath;
                         existingMusic.Duration = CalculateDuration(filePath);
                         existingMusic.Description = description;
                         existingMusic.Updated = DateTime.Now;

                         context.SaveChanges();
                    }
               }
          }

          public void DeleteMusic(int musicId)
          {
               using (var context = new MusicContext())
               {
                    var musicToDelete = context.Musics.Find(musicId);
                    if (musicToDelete != null)
                    {
                         context.Musics.Remove(musicToDelete);
                         context.SaveChanges();
                    }
               }
          }

          private TimeSpan CalculateDuration(string filePath)
          {
               try
               {
                    using (var file = TagLib.File.Create(filePath))
                    {
                         var tagFile = TagLib.File.Create(file.Name);
                         if (tagFile != null && tagFile.Properties != null)
                         {
                              return tagFile.Properties.Duration;
                         }
                    }
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"Error calculating duration: {ex.Message}");
               }

               return TimeSpan.FromMinutes(5);
          }

          public List<Musics> GetMostPlayedDaily()
          {
               var now = DateTime.Now;
               return this.Musics
                   .Where(m => (now - m.LastPlayedTime).TotalDays < 1)
                   .OrderByDescending(m => m.DailyListenCount)
                   .ToList();
          }

          public List<Musics> GetMostPlayedWeekly()
          {
               var now = DateTime.Now;
               return this.Musics
                   .Where(m => (now - m.LastPlayedTime).TotalDays < 7)
                   .OrderByDescending(m => m.WeeklyListenCount)
                   .ToList();
          }

          public List<Musics> GetMostPlayedMonthly()
          {
               var now = DateTime.Now;
               return this.Musics
                   .Where(m => (now - m.LastPlayedTime).TotalDays < 30)
                   .OrderByDescending(m => m.MonthlyListenCount)
                   .ToList();
          }



     }
}
