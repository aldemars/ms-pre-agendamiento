using System.Collections.Generic;

namespace ms_pre_agendamiento.Dto
{
    public class UserResponse
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string Role { set; get; }
        public List <AppointmentResponse> Appointments{ set; get; } 
    }
}