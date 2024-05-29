using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Music
{
     public class Genres
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int GenreId { get; set; }
          [Required]
          public string Name { get; set; }
          public string Description { get; set; }
          public ICollection<Musics> Musics { get; set; }
          public int DailyListenCount { get; set; }
          public int WeeklyListenCount { get; set; }
          public int MonthlyListenCount { get; set; }
     }
}
