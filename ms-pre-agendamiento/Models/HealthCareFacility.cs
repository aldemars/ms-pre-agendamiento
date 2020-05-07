using System;

namespace ms_pre_agendamiento.Models
{
    public class HealthCareFacility
    {
        public int Id { set; get; }
        
        public string Name { set; get; }
        
        public string Address { set; get; }
        
        public int WorkingHoursFrom { set; get; }
        
        public int WorkingHoursTo { set; get; }
    }
}