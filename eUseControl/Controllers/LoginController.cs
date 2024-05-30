using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using eUseControl.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using eUseControl.BusinessLogic.DBModel;
using AutoMapper;

namespace eUseControl.Controllers
{
     public class LoginController : BaseController
     {
          private readonly ISession _session;
          public LoginController()
          {
               var bl = new BussinesLogic();
               _session = bl.GetSessionBL();
          }

          // GET: Login
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
                    Mapper.Initialize(cfg => cfg.CreateMap<UserLogin, UserLoginData>());
                    var data = Mapper.Map<UserLoginData>(login);

                    data.LoginIp = Request.UserHostAddress;
                    data.LoginDateTime = DateTime.Now;

                    var userLogin = _session.UserLogin(data);
                    if (userLogin.Status)
                    {
                         HttpCookie cookie = _session.GenCookie(login.Credential);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                         if (IsAdmin())
                         {
                              return RedirectToAction("AdminDashboard", "Home");
                         }
                         else
                         {
                              return RedirectToAction("Home", "Home");
                         }
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