using eUseControl.BusinessLogic.Logic;
using eUseControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace eUseControl.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
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
                    int recordsCreate = UserProcessor.CreateUser(model.FirstName, 
                         model.LastName, 
                         model.EmailAddress, 
                         model.Password);
                    return RedirectToAction("Home", "Home");
               }

               return View();
          }

     }
}