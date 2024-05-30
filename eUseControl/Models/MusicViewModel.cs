using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eUseControl.Models
{
     public class MusicViewModel
     {
          [Required(ErrorMessage = "Please enter music title")]
          public string Title { get; set; }

          [Required]
          public string MusicName { get; set; }

          [Required]
          public string ImageName { get; set; }

          [Required(ErrorMessage = "Please select a genre.")]
          public int Genre { get; set; }

          public string Description { get; set; }
     }
}