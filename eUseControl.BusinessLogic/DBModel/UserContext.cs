using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
     public class UserContext : DbContext
     {
          public UserContext() : base("name=MVCMRSUTWEBDB")//string conectare la baza de date in web.config
          {
          }

          public DbSet<UserSignUp> Users { get; set; }

          public static int CreateUser(string firstName, string lastName, string userName, string emailAddress, string password, string ip)
          {
               using (var context = new UserContext())
               {
                    // Create a new UserSignUp object
                    var newUser = new UserSignUp
                    {
                         FirstName = firstName,
                         LastName = lastName,
                         UserName = userName,
                         EmailAddress = emailAddress,
                         Password = password,
                         CreatedOn = DateTime.Now,
                         Role = URole.User,
                         LoginIp = ip,
                    };

                    // Add the new user to the Users DbSet in the context
                    context.Users.Add(newUser);

                    // Save changes to the database
                    context.SaveChanges();

                    // Return the ID of the newly created user (if your UserSignUp model has an ID property)
                    return newUser.UserId;
               }
          }


          public List<UserSignUp> LoadUsers()
          {
               using (var context = new UserContext())
               {
                    // Query to load all users from the database
                    return context.Users.ToList();
               }
          }


     }
}
