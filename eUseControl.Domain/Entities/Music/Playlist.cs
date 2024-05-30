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
     public class Playlists
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int PlaylistId { get; set; }
          [Required]
          public string Name { get; set; }
          public string Description { get; set; }
          public ICollection<Musics> Musics { get; set; }
          public int DailyListenCount { get; set; }
          public int WeeklyListenCount { get; set; }
          public int MonthlyListenCount { get; set; }
          public int UserSignUpId { get; set; }
          public UserSignUp UserSignUp { get; set; }
     }
}
