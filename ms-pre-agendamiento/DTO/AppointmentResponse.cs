using System;
using AutoMapper.Configuration.Conventions;

namespace ms_pre_agendamiento.Dto
{
    public class AppointmentResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        
        [MapTo("Hour")]
        public AppointmentTime Time { get; set; }
        public string SlotId { get; set; }
        public String HealthcareFacility { get; set; }
        
        public string Hour
        {
            get
            {
                string minutes = (this.Time.Minutes < 10) ? $"0{this.Time.Minutes}" : $"{this.Time.Minutes}";
                return $"{this.Time.Hours}:{minutes}";
            }
        }
        
        public string Type { get; set; }
    }

    public class AppointmentTime
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }

}