using System.Collections.Generic;

namespace ms_pre_agendamiento
{
    public interface IScheduledCalendarBlockRepository
    {
        List<ScheduledCalendarBlock> GetScheduledBlocksMock();
    }
}