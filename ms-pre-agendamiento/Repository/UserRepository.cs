namespace Ms_pre_agendamiento.Repository
{
    using System.Linq;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Ms_pre_agendamiento.Models;

    public class UserRepository : EntityBaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public User GetById(int id) =>
            ExecuteCommand(_connectionString, connection =>
                connection.Query<User>(UserCommand.GetById,
                        new {@Id = id})
                    .SingleOrDefault());

        public User GetByUserNameAndPassword(string username, string password) =>
            ExecuteCommand(_connectionString, connection =>
                connection.Query<User>(UserCommand.GetByUserNameAndPassword,
                        new {@Id = username, @Password = password})
                    .SingleOrDefault());

        public bool IsValid(DTO.User user) => GetByUserNameAndPassword(user.Name, user.Password) != null;
    }

    public class UserCommand
    {
        public static string GetById => "Select * From sel_user where Id= @Id";

        public static string GetByUserNameAndPassword =>
            "Select * From sel_user where name= @Name  AND password= @Password";
    }
}