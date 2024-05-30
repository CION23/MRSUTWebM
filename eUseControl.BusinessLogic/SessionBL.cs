using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Helpers;

namespace eUseControl.BusinessLogic
{
     public class SessionBL : UserApi, ISession
     {
          public ULoginResp UserLogin(UserLoginData data)
          {
               return UserLoginAction(data);
          }
          public HttpCookie GenCookie(string loginCredential)
          {
               return Cookie(loginCredential);
          }

          public UserMinimal GetUserByCookie(string apiCookieValue)
          {
               return UserCookie(apiCookieValue);
          }
     }
}
