using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Music
{
     public class Musics
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int MusicId { get; set; }
          [Required]
          public string Title { get; set; }
          [Required]
          public string FilePath { get; set; }
          [Required]
          public string ImagePath { get; set; }
          [Required]
          public TimeSpan Duration { get; set; }
          [Required]
          public DateTime Created { get; set; }
          [Required]
          public DateTime Updated { get; set; }
          public string Description { get; set; }
          public ICollection<Playlists> Playlists { get; set; }
          public ICollection<Genres> Genres { get; set; }
          public DateTime LastPlayedTime { get; set; }
          public int DailyListenCount { get; set; }
          public int WeeklyListenCount { get; set; }
          public int MonthlyListenCount { get; set; }
          public int UserSignUpId { get; set; }
          public UserSignUp UserSignUp { get; set; }
     }
}
