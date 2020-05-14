using System.Collections.Generic;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Repository
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAppointmentsByCenterId(int centerId);
        List<Appointment> GetAppointmentsByUserId(int userId);

        List<Appointment> GetUserAndCenterAppointments(int userId, int healthcareFacilityId);
    }
}