using System;
using System.Collections.Generic;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento
{
    public class ScheduledCalendarBlockRepository : IScheduledCalendarBlockRepository
    {
        public List<TimeSlot> GetScheduledBlocksMock()
        {
            return new List<TimeSlot>
            {
                new TimeSlot() {Date = DateTime.Now, HourFrom = 13, HourTo = 14}
            };
        }
    }
}