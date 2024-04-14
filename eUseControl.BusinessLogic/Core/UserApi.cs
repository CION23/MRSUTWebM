using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Core
{
     public class UserApi
     {
          internal ULoginResp UserLoginAction(ULoginData data)
          {
               UDbTable user;

               using (var db = new UserContext())
               {
                    user = db.Users.FirstOrDefault(u => u.Username == data.Credential);
               }

               using (var db = new UserContext())
               {
                    user = (from u in db.Users where u.Username == data.Credential select u).FirstOrDefault();
               }


               if (user != null)
               {

               }

               return new ULoginResp();
          }
     }
}
