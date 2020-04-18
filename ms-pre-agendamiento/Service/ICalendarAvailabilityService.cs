using System.Collections.Generic;
using Ms_pre_agendamiento.Models;

namespace Ms_pre_agendamiento
{
    public interface ICalendarAvailabilityService
    {
        IEnumerable<TimeSlot> GetAvailableBlocks();
    }
}