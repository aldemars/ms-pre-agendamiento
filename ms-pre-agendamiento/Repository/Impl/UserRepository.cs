using System;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Repository
{
    public class UserRepository : IUserRepository
    {
        private IRepositoryCommandExecuter _command;

        public UserRepository(IRepositoryCommandExecuter command)
        {
            _command = command;
        }

        public User GetById(int id)
        {
            var user = _command.ExecuteCommand<User>(conn =>
                conn.Query<User>(UserCommand.GetById, new {@Id = id}).SingleOrDefault());
            return user;
        }

        public User GetByUserNameAndPassword(string username, string password)
        {
            var parameter = new {@Name = username, @Password = password};
            var user = _command.ExecuteCommand<User>(conn =>
                conn.Query<User>(UserCommand.GetByUserNameAndPassword, parameter).SingleOrDefault());
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

        public static string GetByUserNameAndPassword =>
            "Select * From sel_user where name= @Name  AND password= @Password";
    }
}