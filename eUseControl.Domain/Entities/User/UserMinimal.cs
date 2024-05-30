using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.User
{
     public class UserMinimal
     {
          public int UserId { get; set; }
          public string Username { get; set; }
          public string ArtistName { get; set; }
          public string EmailAddress { get; set; }
          public DateTime LastLogin { get; set; }
          public string LasIp { get; set; }
          public URole Role { get; set; }
     }
}
