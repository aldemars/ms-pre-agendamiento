using System;
using AutoMapper.Configuration.Conventions;

namespace ms_pre_agendamiento.Dto
{
    public class AppointmentResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        
        [MapTo("Hour")]
        public AppointmentTime Time { get; set; }
        public string SlotId { get; set; }
        public String HealthcareFacility { get; set; }
    }

    public class AppointmentTime
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }

}