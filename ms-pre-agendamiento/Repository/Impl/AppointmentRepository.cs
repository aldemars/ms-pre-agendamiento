using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.FluentMap.Mapping;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Repository.Impl
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private IRepositoryCommandExecuter _command;

        public AppointmentRepository(IRepositoryCommandExecuter command)
        {
            _command = command;
        }

        public List<Appointment> GetAppointmentsByCenterId(int centerId)
        {
            return _command.ExecuteCommand<List<Appointment>>(conn =>
                conn.Query<Appointment>(AppointmentCommand.GetAppointmentsByCenterId, new {@CenterId = centerId}).AsList());
        }

        public List<Appointment> GetAppointmentsByUserId(int userId)
        {
            return _command.ExecuteCommand<List<Appointment>>(conn =>
                conn.Query<Appointment>(AppointmentCommand.GetAppointmentsByUserId, new {@UserId = userId}).AsList());
        }
        
        public List<Appointment> GetUserAndCenterAppointments(int userId, int healthcareFacilityId)
        {
            List<Appointment> appointments = _command.ExecuteCommand<List<Appointment>>(conn =>
            {
                var result = conn.QueryMultiple(AppointmentCommand.GetUserAndCenterAppointments, new {@UserId = userId, @HealthcareFacilityId=healthcareFacilityId});
                var list1 = result.Read<Appointment>().AsList();
                var list2 = result.Read<Appointment>().AsList();
                return list1.Concat(list2).AsList();
            });
            return appointments;
        }
    }

    internal class AppointmentMap : EntityMap<Appointment>
    {
        internal AppointmentMap()
        {
            Map(u => u.SlotId).ToColumn("slot_id");
            Map(u => u.HealthcareFacilityId).ToColumn("healthcare_facility_id");
        }
    }
    public class AppointmentCommand
    {
        public static string GetById => "Select * From appointment where Id= @Id";

        public static string GetAppointmentsByCenterId =>
            @"Select id, slot_id, 'busy' as description, date, hour, healthcare_facility_id, 'centerAppointment' as type 
                  From appointment where healthcare_facility_id= @HealthcareFacilityId";

        public static string GetAppointmentsByUserId =>
            @"Select id, slot_id, description, date, hour, healthcare_facility_id, 'userAppointment' as type 
                    From appointment where user_id= @UserId";
        
        public static string GetUserAndCenterAppointments =>
            @"Select *, 'userAppointment' as type From appointment where user_id= @UserId;
              Select id, slot_id, 'Busy' as description, date, hour, 'centerAppointment' as type 
                    From appointment where healthcare_facility_id= @CenterId AND user_id<> @UserId";
    }
}