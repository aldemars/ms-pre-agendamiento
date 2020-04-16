using System;
using System.Collections.Generic;
using System.Globalization;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento
{
    public class AllCalendarTimeSlotsRepository : IAllCalendarTimeSlotsRepository
    {
        public IEnumerable<TimeSlot> GetAllTimeSlotsMock()
        {
            var spainCulture = CultureInfo.CreateSpecificCulture("es-ES");
            const string formatSpecifier = "d";
            
            return new List<TimeSlot>
            {
                new TimeSlot() {Date = DateTime.Now.ToString(formatSpecifier, spainCulture), HourFrom = 10, HourTo = 11},
                new TimeSlot() {Date = DateTime.Now.ToString(formatSpecifier, spainCulture), HourFrom = 12, HourTo = 13},
                new TimeSlot() {Date = DateTime.Now.ToString(formatSpecifier, spainCulture), HourFrom = 13, HourTo = 14},
                new TimeSlot() {Date = DateTime.Now.ToString(formatSpecifier, spainCulture), HourFrom = 15, HourTo = 16}
            };
        }
    }
}