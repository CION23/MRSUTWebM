using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Enums;
using eUseControl.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace eUseControl.Controllers
{
     public class BaseController : Controller
     {
          private readonly ISession _session;

          public BaseController()
          {
               var bl = new BussinesLogic();
               _session = bl.GetSessionBL();
          }

          public void SessionStatus()
          {
               var apiCookie = Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    var profile = _session.GetUserByCookie(apiCookie.Value);
                    if (profile != null)
                    {
                         System.Web.HttpContext.Current.SetMySessionObject(profile);
                         System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                         System.Web.HttpContext.Current.Session["UserName"] = profile.Username;
                    }
                    else
                    {
                         System.Web.HttpContext.Current.Session.Clear();
                         System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";

                         using (var db = new SessionContext())
                         {
                              var session = db.Sessions.FirstOrDefault(s => s.CookieString == apiCookie.Value);
                              if (session != null)
                              {
                                   db.Sessions.Remove(session);
                                   db.SaveChanges();
                              }
                         }

                         apiCookie.Expires = DateTime.Now.AddDays(-1);
                         Response.Cookies.Add(apiCookie);
                    }
               }
               else
               {
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
               }

          }


          public bool IsAdmin()
          {
               return AuthorizationHelper.IsAdmin(_session);
          }

          public bool IsUser()
          {
               return AuthorizationHelper.IsUser(_session);
          }
     }
}