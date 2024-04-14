using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eUseControl.Models
{
     public class UserLogin
     {
          [Required]
          [Display(Name = "Username or Email")]
          public string Credential { get; set; }

          [Required]
          [DataType(DataType.Password)]
          [Display(Name = "Password")]
          public string Password { get; set; }
     }
}