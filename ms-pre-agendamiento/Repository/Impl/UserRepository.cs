using System.Linq;
using Dapper;
using Dapper.FluentMap;
using Dapper.FluentMap.Mapping;
using ms_pre_agendamiento.Dto;
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

        public User GetUser(LoginRequest loginRequest)
        {
            return GetByUserNameAndPassword(loginRequest.Name, loginRequest.Password);
        }

        public User GetUserAppointmentsById(int id)
        {
            var user = _command.ExecuteCommand<User>(conn =>
            {
                var result = conn.QueryMultiple(UserCommand.GetUserAppointmentsById, new {@Id = id});
                var user = result.ReadSingle<User>();
                user.Appointments = result.Read<Appointment>().AsList();
                return user;
            });
            return user;
        }

        public void HealthCheck()
        {
            _command.HealthCheck();
        }
    }
    
    public class UserCommand
    {
        public static string GetById => "Select * From sel_user where Id= @Id";

        public static string GetByUserNameAndPassword =>
            "Select * From sel_user where name= @Name  AND password= @Password";
        
        public static string GetUserAppointmentsById => 
            @"Select * From sel_user where Id= @Id;
            Select id, slot_id, description, date, hour, healthcare_facility_id, 'userAppointment' as type From appointment where user_id= @Id";
    }
}