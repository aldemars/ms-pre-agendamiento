using System;
using System.Collections.Generic;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento
{
    public class AllCalendarTimeSlotsRepository : IAllCalendarTimeSlotsRepository
    {
        public IEnumerable<TimeSlot> GetAllTimeSlotsMock()
        {
            return new List<TimeSlot>
            {
                new TimeSlot() {Date = DateTime.Now.ToShortDateString(), HourFrom = 10, HourTo = 11},
                new TimeSlot() {Date = DateTime.Now.ToShortDateString(), HourFrom = 12, HourTo = 13},
                new TimeSlot() {Date = DateTime.Now.ToShortDateString(), HourFrom = 13, HourTo = 14},
                new TimeSlot() {Date = DateTime.Now.ToShortDateString(), HourFrom = 15, HourTo = 16}
            };
        }
    }
}