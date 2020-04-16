using System;
using System.ComponentModel.DataAnnotations;

namespace ms_pre_agendamiento.Models
{
    public class TimeSlot
    {
        public string Date { get; set; }

        public int HourFrom { get; set; }

        public int HourTo { get; set; }
    }
}