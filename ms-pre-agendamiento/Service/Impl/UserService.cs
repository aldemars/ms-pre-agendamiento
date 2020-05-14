using System.Collections.Generic;
using System.Linq;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Service.Impl
{
    public class UserService : IUserService
    {
        private IAppointmentRepository _appointmentRepository;

        public UserService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public List<Appointment> GetUserAndCenterAppointments(int userId, int healthcareFacilityId)
        {
            List<Appointment> centerAppointmentsList = _appointmentRepository.GetUserAndCenterAppointments(userId,healthcareFacilityId);
            return centerAppointmentsList;
        }
    }
}