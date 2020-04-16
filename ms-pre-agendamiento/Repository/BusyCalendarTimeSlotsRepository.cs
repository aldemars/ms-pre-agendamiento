using System;
using System.Collections.Generic;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento
{
    public class BusyCalendarTimeSlotsRepository : IBusyCalendarTimeSlotsRepository
    {
        public IEnumerable<TimeSlot> GetScheduledBlocksMock()
        {
            return new List<TimeSlot>
            {
                new TimeSlot() {Date = DateTime.Now.ToShortDateString(), HourFrom = 13, HourTo = 14}
            };
        }
    }
}