using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Enums;
using eUseControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserSignUp = eUseControl.Domain.Entities.User.UserSignUp;


namespace eUseControl.Controllers
{
    public class HomeController : Controller
    {
          private readonly UserContext _context;

          public HomeController()
          {
               _context = new UserContext();
          }
          public ActionResult Home()
          {
               return View();
          }
          public ActionResult Search()
          {
               return View();
          }

          public ActionResult SignUp()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult SignUp(UserSignUp model)
          {
               if (ModelState.IsValid)
               {
                    // Attempt to retrieve the IP address from various server variables
                    string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (string.IsNullOrEmpty(ipAddress))
                    {
                         ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                    }

                    // Create a new user using Entity Framework
                    var newUser = new UserSignUp
                    {
                         FirstName = model.FirstName,
                         LastName = model.LastName,
                         UserName = model.UserName,
                         EmailAddress = model.EmailAddress,
                         Password = model.Password,
                         CreatedOn = DateTime.Now,
                         Role = URole.User,
                         LoginIp = ipAddress,
                    };

                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    return RedirectToAction("Home", "Home");
               }

               return View();
          }

     }
}