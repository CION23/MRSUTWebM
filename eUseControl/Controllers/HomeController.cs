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
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic;
using System.Web.Management;


namespace eUseControl.Controllers
{
    public class HomeController : BaseController
     {
          private readonly UserContext _context;
          public HomeController()
          {
               _context = new UserContext();
          }

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
               var apiCookie = Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    using (var db = new SessionContext())
                    {
                         var session = db.Sessions.FirstOrDefault(s => s.CookieString == apiCookie.Value);
                         if (session != null)
                         {
                              db.Sessions.Remove(session);
                              db.SaveChanges();
                         }
                    }
                    System.Web.HttpContext.Current.Session.Clear();
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";

                    apiCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(apiCookie);
               }
               return RedirectToAction("Home", "Home");
          }

          [ValidateAntiForgeryToken]
          [HttpPost]
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

          public ActionResult AdminDashboard()
          {
               if (IsAdmin())
               {
                    return View();
               }
               else
               {
                    return RedirectToAction("Home", "Home");
               }
          }

          public ActionResult Control()
          {
               if (IsAdmin())
               {
                    IEnumerable<eUseControl.Domain.Entities.User.UserSignUp> users;
                    using (var context = new UserContext())
                    {
                         users = context.Users.ToList();
                    }

                    return View(users);
               }
               else
               {
                    return RedirectToAction("Home", "Home");
               }
               
          }

          [HttpPost]
          public ActionResult Modify(int userId, string firstName, string lastName, string userName, string emailAddress, string password, string ip)
          {
               if (IsAdmin())
               {
                    var success = UserContext.ModifyUserData(userId, firstName, lastName, userName, emailAddress, password, ip);

                    if (success)
                    {
                         return RedirectToAction("Control", "Home");
                    }
                    else
                    {
                         ViewBag.ErrorMessage = "Failed to modify user.";
                         var users = _context.LoadUsers();
                         return View("Control", users);
                    }
               }
               else
               {
                    return RedirectToAction("Home","Home");
               }
          }

          [HttpPost]
          public ActionResult Delete(int userId)
          {
               if (IsAdmin())
               {
                    bool deletionSuccess = UserContext.DeleteUser(userId);

                    if (deletionSuccess)
                    {
                         return RedirectToAction("Control", "Home");
                    }
                    else
                    {
                         ViewBag.ErrorMessage = "Failed to Delete user.";
                         var userContext = new UserContext();
                         var users = userContext.LoadUsers();
                         return View("Control", users);
                    }
               }
               else
               {
                    return RedirectToAction("Home","Home");
               }
               
          }
    }
}