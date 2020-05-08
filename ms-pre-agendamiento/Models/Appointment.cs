using System;

namespace ms_pre_agendamiento.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Hour { get; set; }
        public string Slot_Id { get; set; }
        public int Healthcare_Facility_Id { get; set; }
    }
}