﻿using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Atribute
{
     public class AuthorizationHelper
     {
          public static bool IsAdmin(ISession session)
          {
               var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    var profile = session.GetUserByCookie(apiCookie.Value);
                    if (profile != null && profile.Role == URole.Admin)
                    {
                         return true;
                    }
               }
               return false;
          }
     }
}