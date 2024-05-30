using eUseControl.Domain.Entities.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Models
{
     public class MostPlayedViewModel
     {
          public List<Musics> Daily { get; set; }
          public List<Musics> Weekly { get; set; }
          public List<Musics> Monthly { get; set; }
     }
}