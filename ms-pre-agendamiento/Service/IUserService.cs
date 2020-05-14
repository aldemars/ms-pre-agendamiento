using System.Collections.Generic;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Service
{
    public interface IUserService
    {
        List<Appointment> GetUserAndCenterAppointments(int userId, int healthcareFacilityId);
    }
}