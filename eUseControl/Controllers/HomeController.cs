using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities;
using UserSignUp = eUseControl.Models.UserSignUp;
using AutoMapper;
using eUseControl.Extension;


namespace eUseControl.Controllers
{
    public class HomeController : BaseController
     {
          public ActionResult Home()
          {
               SessionStatus();
               if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
               {
                    var user = System.Web.HttpContext.Current.GetMySessionObject();
                    UserData u = new UserData
                    {
                         Username = user.Username,
                    };

                    return View(u);
               }
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

          public ActionResult LogOut()
          {
               ClearSessionAndCookies();
               return RedirectToAction("Home","Home");
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult SignUp(UserSignUp model)
          {
               if (ModelState.IsValid)
               {
                    using (var userContext = new UserContext())
                    {
                         // Check if username or email already exists
                         bool userExists = userContext.Users.Any(u => u.UserName == model.UserName);
                         bool emailExists = userContext.Users.Any(u => u.EmailAddress == model.EmailAddress);

                         if (userExists)
                         {
                              ModelState.AddModelError("UserName", "Username already in use.");
                         }

                         if (emailExists)
                         {
                              ModelState.AddModelError("EmailAddress", "Email address already in use.");
                         }

                         if (!userExists && !emailExists)
                         {
                              string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                              if (string.IsNullOrEmpty(ipAddress))
                              {
                                   ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                              }

                              Mapper.Initialize(cfg => cfg.CreateMap<eUseControl.Models.UserSignUp, eUseControl.Domain.Entities.User.UserSignUp > ());

                              UserContext.CreateUser(model.FirstName, model.LastName, model.UserName, model.EmailAddress, model.Password, ipAddress);
                              return RedirectToAction("Home", "Home");
                         }
                    }
               }
               return View(model);
          }
     }
}