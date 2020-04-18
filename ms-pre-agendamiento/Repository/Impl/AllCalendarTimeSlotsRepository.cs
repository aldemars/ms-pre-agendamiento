using System.Collections.Generic;
using Ms_pre_agendamiento.Models;
using static System.DateTime;
using static System.Globalization.CultureInfo;

namespace Ms_pre_agendamiento.Repository.Impl
{
    public class AllCalendarTimeSlotsRepository : IAllCalendarTimeSlotsRepository
    {
        public IEnumerable<TimeSlot> GetAllTimeSlotsMock()
        {
            var spainCulture = CreateSpecificCulture("es-ES");
            const string formatSpecifier = "d";

            return new List<TimeSlot>
            {
                new TimeSlot()
                    {Date = Now.ToString(formatSpecifier, spainCulture), HourFrom = 10, HourTo = 11},
                new TimeSlot()
                    {Date = Now.ToString(formatSpecifier, spainCulture), HourFrom = 12, HourTo = 13},
                new TimeSlot()
                    {Date = Now.ToString(formatSpecifier, spainCulture), HourFrom = 13, HourTo = 14},
                new TimeSlot()
                    {Date = Now.ToString(formatSpecifier, spainCulture), HourFrom = 15, HourTo = 16}
            };
        }
    }
}