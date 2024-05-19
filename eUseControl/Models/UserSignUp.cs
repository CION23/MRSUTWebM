using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eUseControl.Models
{
     public class UserSignUp
     {
          [Display(Name = "First Name")]
          [Required(ErrorMessage = "Fill Up First Name")]
          public string FirstName { get; set; }

          [Display(Name = "Last Name")]
          [Required(ErrorMessage = "Fill Up Last Name")]
          public string LastName { get; set; }

          [Display(Name = "User Name")]
          [Required(ErrorMessage = "Fill Up User Name")]
          public string UserName { get; set; }

          [Display(Name = "Email Address")]
          [Required(ErrorMessage = "Fill Up Email Address")]
          public string EmailAddress { get; set; }

          [Display(Name = "Confirm Email Address")]
          [Compare("EmailAddress", ErrorMessage ="The Email and Confirm Email must match")]
          public string ConfirmEmailAddress { get; set; }

          [Display(Name = "Password")]
          [Required(ErrorMessage = "Fill Up Password")]
          public string Password { get; set; }

          [Display(Name = "Confirm Password")]
          [Compare("Password", ErrorMessage ="Password and confirm password don't match")]
          public string ConfirmPassword { get; set; }

     }
}