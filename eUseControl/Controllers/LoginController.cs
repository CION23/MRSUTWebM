using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using eUseControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace eUseControl.Controllers
{
    public class LoginController : Controller
    {
          private readonly ISession _session;
          public LoginController()
          {
               var bl = new BussinesLogic();
               _session = bl.GetSessionBL();
          }

          public ActionResult Login()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Login(UserLogin login)
          {
               if (ModelState.IsValid)
               {
                    ULoginData data = new ULoginData
                    {
                         Credential = login.Credential,
                         Password = login.Password,
                         LoginIp = Request.UserHostAddress,
                         LoginDateTime = DateTime.Now
                    };

                    var userLogin = _session.UserLogin(data);
                    if(userLogin.Status)
                    {
                         //ADD CO)KIE

                         return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                         ModelState.AddModelError("", userLogin.StatusMsg);
                         return View();
                    }
               }

               return View();
          }
     }
}