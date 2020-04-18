using System.Collections.Generic;
using Ms_pre_agendamiento.Models;
using static System.DateTime;
using static System.Globalization.CultureInfo;

namespace Ms_pre_agendamiento.Repository.Impl
{
    public class BusyCalendarTimeSlotsRepository : IBusyCalendarTimeSlotsRepository
    {
        public IEnumerable<TimeSlot> GetScheduledBlocksMock()
        {
            var spainCulture = CreateSpecificCulture("es-ES");
            const string formatSpecifier = "d";

            return new List<TimeSlot>
            {
                new TimeSlot()
                    {Date = Now.ToString(formatSpecifier, spainCulture), HourFrom = 13, HourTo = 14}
            };
        }
    }
}