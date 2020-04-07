using System;
using System.Collections.Generic;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento
{
    public class AllCalendarBlockRepository:IAllCalendarBlockRepository
    {
        public List<TimeSlot> GetAllSlotsMock()
        {
            return new List<TimeSlot>
            {
                new TimeSlot() {Date = DateTime.Now, HourFrom = 10, HourTo = 11},
                new TimeSlot() {Date = DateTime.Now, HourFrom = 12, HourTo = 13},
                new TimeSlot() {Date = DateTime.Now, HourFrom = 13, HourTo = 14},
                new TimeSlot() {Date = DateTime.Now, HourFrom = 15, HourTo = 16}
            };
        }
    }
}