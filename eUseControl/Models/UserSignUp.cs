﻿using System;
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

          [Required(ErrorMessage = "Fill Up Email Address")]
          public string EmailAddress { get; set; }

          [Compare("EmailAddress", ErrorMessage ="The Email and Confirm Email must match")]
          public string ConfirmEmailAddress { get; set; }

          [Required(ErrorMessage = "Fill Up Password")]
          public string Password { get; set; }

          [Compare("Password", ErrorMessage ="Password and confirm password don't match")]
          public string ConfirmPassword { get; set; }

     }
}