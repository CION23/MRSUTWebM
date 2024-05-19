using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.User
{
     public partial class UserSignUp
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int UserId { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string UserName { get; set; }
          public string EmailAddress { get; set; }
          public string Password { get; set; }

          [DataType(DataType.Date)]
          public DateTime CreatedOn { get; set; }

          [StringLength(30)]
          public string LoginIp { get; set; }

          public URole Role { get; set; }
     }
}
