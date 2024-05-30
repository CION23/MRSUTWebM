using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Music;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBIntializer
{
     public class GenresInitializer : DropCreateDatabaseIfModelChanges<MusicContext>
     {
          protected override void Seed(MusicContext context)
          {
               if (!context.Genres.Any())
               {
                    context.Genres.Add(new Genres { Name = "Rock" });
                    context.Genres.Add(new Genres { Name = "Pop" });
                    context.Genres.Add(new Genres { Name = "Hip Hop" });
                    context.Genres.Add(new Genres { Name = "Latin" });
                    context.Genres.Add(new Genres { Name = "Chart" });
                    context.Genres.Add(new Genres { Name = "Dance/Electronic" });
                    context.Genres.Add(new Genres { Name = "Mood" });
                    context.Genres.Add(new Genres { Name = "Indie" });
                    context.Genres.Add(new Genres { Name = "Workout" });
                    context.Genres.Add(new Genres { Name = "Discover" });
                    context.Genres.Add(new Genres { Name = "Country" });
                    context.Genres.Add(new Genres { Name = "R&B" });
                    context.Genres.Add(new Genres { Name = "K-pop" });
                    context.Genres.Add(new Genres { Name = "Chill" });
                    context.Genres.Add(new Genres { Name = "Sleep" });
                    context.Genres.Add(new Genres { Name = "Party" });
                    context.Genres.Add(new Genres { Name = "At Home" });
                    context.Genres.Add(new Genres { Name = "Decades" });
                    context.Genres.Add(new Genres { Name = "Love" });
                    context.Genres.Add(new Genres { Name = "Metal" });
                    context.Genres.Add(new Genres { Name = "Jazz" });
                    context.Genres.Add(new Genres { Name = "Wellness" });
                    context.Genres.Add(new Genres { Name = "Gaming" });
                    context.Genres.Add(new Genres { Name = "Folk & Acoustic" });
                    context.Genres.Add(new Genres { Name = "Anime" });
                    context.Genres.Add(new Genres { Name = "Anime Openings" });
                    context.Genres.Add(new Genres { Name = "Focus" });
                    context.Genres.Add(new Genres { Name = "Soul" });
                    context.Genres.Add(new Genres { Name = "Children & Family" });
                    context.Genres.Add(new Genres { Name = "Classical" });
                    context.Genres.Add(new Genres { Name = "TV & Films" });
                    context.Genres.Add(new Genres { Name = "Instrumental" });
                    context.Genres.Add(new Genres { Name = "Punk" });
                    context.Genres.Add(new Genres { Name = "Ambient" });
                    context.Genres.Add(new Genres { Name = "Blues" });
                    context.Genres.Add(new Genres { Name = "Cooking & Dining" });
                    context.Genres.Add(new Genres { Name = "Netflix" });
                    context.Genres.Add(new Genres { Name = "Alternative" });
                    context.Genres.Add(new Genres { Name = "Travel" });
                    context.Genres.Add(new Genres { Name = "Caribbean" });
                    context.Genres.Add(new Genres { Name = "Afro" });
                    context.Genres.Add(new Genres { Name = "SongWriters" });
                    context.Genres.Add(new Genres { Name = "Nature & Noise" });
                    context.Genres.Add(new Genres { Name = "Funk & Disco" });
                    context.Genres.Add(new Genres { Name = "Spotify Singles" });
                    context.Genres.Add(new Genres { Name = "Summer" });
                    context.Genres.Add(new Genres { Name = "EQUAL" });
                    context.Genres.Add(new Genres { Name = "RADAR" });
                    context.Genres.Add(new Genres { Name = "Fresh Finds" });
               }

               base.Seed(context);
          }
     }

}
