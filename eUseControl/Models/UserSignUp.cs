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
          [Required(ErrorMessage = "Fill up first name")]
          public string FirstName { get; set; }

          [Display(Name = "Last Name")]
          [Required(ErrorMessage = "Fill up last name")]
          public string LastName { get; set; }

          [Display(Name = "User Name")]
          [Required(ErrorMessage = "Fill up username")]
          public string UserName { get; set; }

          [Display(Name = "Email Address")]
          [Required(ErrorMessage = "Fill up email address")]
          public string EmailAddress { get; set; }

          [Display(Name = "Reenter your email address")]
          [Compare("EmailAddress", ErrorMessage ="The Email and Confirm Email must match")]
          public string ConfirmEmailAddress { get; set; }

          [Display(Name = "Password")]
          [Required(ErrorMessage = "Fill up password")]
          [DataType(DataType.Password)]
          public string Password { get; set; }

          [Display(Name = "Confirm Password")]
          [Compare("Password", ErrorMessage ="Password and confirm password don't match")]
          [DataType(DataType.Password)]
          public string ConfirmPassword { get; set; }

          [StringLength(30)]
          public string LoginIp { get; set; }

     }
}