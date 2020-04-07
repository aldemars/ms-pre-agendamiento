using System;

namespace ms_pre_agendamiento
{
    public class CalendarAvailabilityService : ICalendarAvailabilityService
    {
        private readonly IScheduledCalendarBlockRepository scheduledCalendarBlockRepository;

        public CalendarAvailabilityService(IScheduledCalendarBlockRepository scheduledCalendarBlockRepository)
        {
            this.scheduledCalendarBlockRepository =
                scheduledCalendarBlockRepository ?? throw new ArgumentNullException("ScheduledCalendarBlockRepository");
        }

        public int GetBlocksSize()
        {
            var block = scheduledCalendarBlockRepository.GetScheduledBlocksMock();
            return block.Count;
        }
    }
}