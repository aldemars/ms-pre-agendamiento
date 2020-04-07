using System.Collections.Generic;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento
{
    public interface IScheduledCalendarBlockRepository
    {
        List<ScheduledCalendarBlock> GetScheduledBlocksMock();
    }
}