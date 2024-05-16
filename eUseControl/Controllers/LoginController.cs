using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.DataAcces;
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
using Dapper;
using System.Configuration;
using eUseControl.BusinessLogic.Logic;

namespace eUseControl.Controllers
{
     public class LoginController : Controller
     {
          public ActionResult Login()
          {
               return View();
          }

          public static bool ValidateLogin(UserLogin login)
          {
               // SQL query to check if the username and password match
               string sql = "SELECT COUNT(*) FROM UserMaster WHERE UserName = @Username AND Password = @Password";

               using (var cnn = new SqlConnection(SqlDataAcces.GetConnectionString()))
               {
                    int count = cnn.QuerySingleOrDefault<int>(sql, new { Username = login.Credential, Password = login.Password });
                    return count > 0; // Return true if there's a match
               }
          }

          [HttpPost]
          public ActionResult Login(UserLogin login)
          {
               bool isValidLogin = ValidateLogin(login); // Changed from UserProcessor.ValidateLogin(login)

               if (isValidLogin)
               {
                    // Redirect to the home page or dashboard upon successful login
                    return RedirectToAction("Index", "Home");
               }
               else
               {
                    // Invalid credentials, show error message
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View();
               }

               /*if (ModelState.IsValid)
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
                         //HttpCookie cookie = _session.GenCookie(login.Credential);
                         //ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                         return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                         ModelState.AddModelError("", userLogin.StatusMsg);
                         return View();
                    }
               }*/
          }

          public ActionResult LogOut()
          {
               return View();
          }

          private readonly ISession _session;
          public LoginController()
          {
               var bl = new BussinesLogic();
               _session = bl.GetSessionBL();
          }
     }
}