﻿using eUseControl.Domain.Entities.User;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Interfaces
{
     public interface ISession
     {
          ULoginResp UserLogin(ULoginData data);

          //HttpCookie GenCookie(string loginCredential);

          //UserMinimal GetUserByCookie(string apiCookieValue);
     }
}