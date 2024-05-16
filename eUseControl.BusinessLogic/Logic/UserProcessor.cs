using Dapper;
using eUseControl.BusinessLogic.DataAcces;
using eUseControl.BusinessLogic.DBModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Logic
{
     public static class UserProcessor
     {
          public static int CreateUser(string firstName, string lastName, string emailAddress, string password)
          {
               Guid userId = Guid.NewGuid();
               int userIdInt = userId.GetHashCode(); // Convert Guid to int

               UserSignUp data = new UserSignUp
               {
                    Id = userIdInt,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = emailAddress,
                    Password = password,
               };

               string sql = @"insert into dbo.UserSignup (Id,FirstName, LastName, EmailAddress, Password)
                              values (@Id, @FirstName, @LastName, @EmailAddress, @Password);";

               return SqlDataAcces.SaveData(sql, data);
          }

          public static List<UserSignUp> LoadUsers()
          {
               string sql = @"select Id, FirstName, LastName, EmailAddress, Password
                              from dbo.UserSignup;";
               
               return SqlDataAcces.LoadData<UserSignUp>(sql);
          }
     }
}
