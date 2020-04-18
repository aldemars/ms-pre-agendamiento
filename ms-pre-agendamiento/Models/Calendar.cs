namespace Ms_pre_agendamiento.Models
{
    using System;
    using System.Collections.Generic;

    public class Calendar
    {
        private DateTime From { get; }

        private DateTime To { get; }

        private List<TimeSlot> AvailableSlots { get; }

        public Calendar(DateTime from, DateTime to, List<TimeSlot> availableTimeSlots)
        {
            From = from;
            To = to;
            AvailableSlots = availableTimeSlots;
        }
    }
}