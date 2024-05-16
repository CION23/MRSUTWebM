using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
     public partial class UserSignUp
     {
          public int Id { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string EmailAddress { get; set; }
          public string Password { get; set; }
     }
}
