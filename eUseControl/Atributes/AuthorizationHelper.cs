using System.Web;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Enums;

public static class AuthorizationHelper
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

     public static bool IsUser(ISession session)
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

     public static bool IsArtist(ISession session)
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
