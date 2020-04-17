using System;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Repository
{
    public class UserRepository : EntityBaseRepository,IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public User GetById(int id)
        {
            var user = ExecuteCommand<User>(ConnectionString, conn =>
                conn.Query<User>(UserCommand.GetById, new { @Id = id }).SingleOrDefault());
            return user;
        }

        public User GetByUserNameAndPassword(string username, string password)
        {
            var user = ExecuteCommand<User>(ConnectionString, conn =>
                conn.Query<User>(UserCommand.GetByUserNameAndPassword, new { @Id = username,@Password = password }).SingleOrDefault());
            return user;
        }

        public Boolean isValid(DTO.User user)
        {
            return GetByUserNameAndPassword(user.Name, user.Password) != null;
        }
    }
    public class UserCommand
    {
        public static string GetById => "Select * From sel_user where Id= @Id";
        public static string GetByUserNameAndPassword => "Select * From sel_user where name= @Name  AND password= @Password";
    }
}