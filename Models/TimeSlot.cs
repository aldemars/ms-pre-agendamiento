using System;

namespace ms_pre_agendamiento.Models
{
    public class TimeSlot
    {
        public DateTime Date { get; set; }

        public int HourFrom { get; set; }

        public int HourTo { get; set; }
    }
}