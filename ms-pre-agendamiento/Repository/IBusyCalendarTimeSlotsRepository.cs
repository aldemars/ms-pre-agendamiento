using System.Collections.Generic;
using Ms_pre_agendamiento.Models;

namespace Ms_pre_agendamiento
{
    public interface IBusyCalendarTimeSlotsRepository
    {
        IEnumerable<TimeSlot> GetScheduledBlocksMock();
    }
}