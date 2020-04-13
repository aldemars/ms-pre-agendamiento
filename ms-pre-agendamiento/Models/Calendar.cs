using System;
using System.Collections.Generic;

namespace ms_pre_agendamiento.Models
{
    public class Calendar
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<TimeSlot> AvailableSlots { get; set; }
        
        public Calendar(DateTime from, DateTime to, List<TimeSlot> availableTimeSlots)
        {
            From = from;
            To = to;
            AvailableSlots = availableTimeSlots;
        }
    }
}