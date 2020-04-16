using System;
using System.Collections.Generic;
using System.Globalization;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento
{
    public class BusyCalendarTimeSlotsRepository : IBusyCalendarTimeSlotsRepository
    {
        public IEnumerable<TimeSlot> GetScheduledBlocksMock()
        {
            var spainCulture = CultureInfo.CreateSpecificCulture("es-ES");
            const string formatSpecifier = "d";

            return new List<TimeSlot>
            {
                new TimeSlot() {Date = DateTime.Now.ToString(formatSpecifier, spainCulture), HourFrom = 13, HourTo = 14}
            };
        }
    }
}