using System;
using System.Collections.Generic;
using System.Linq;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento
{
    public class CalendarAvailabilityService : ICalendarAvailabilityService
    {
        private readonly IScheduledCalendarBlockRepository scheduledCalendarBlockRepository;
        private readonly IAllCalendarBlockRepository allCalendarBlockRepository;

        public CalendarAvailabilityService(IScheduledCalendarBlockRepository scheduledCalendarBlockRepository,
            IAllCalendarBlockRepository allCalendarBlockRepository)
        {
            this.scheduledCalendarBlockRepository =
                scheduledCalendarBlockRepository ?? throw new ArgumentNullException("ScheduledCalendarBlockRepository");
            this.allCalendarBlockRepository =
                allCalendarBlockRepository ?? throw new ArgumentNullException("AllCalendarBlockRepository");
        }

        public int GetBlocksSize()
        {
            var block = scheduledCalendarBlockRepository.GetScheduledBlocksMock();
            return block.Count;
        }
        
        public IEnumerable<TimeSlot> GetAvailableBlocks()
        {
            var allSlots = allCalendarBlockRepository.GetAllSlotsMock();
            var busySlots = scheduledCalendarBlockRepository.GetScheduledBlocksMock();
            return allSlots.Except(busySlots, new TimeSlotComparator());
        }
    }
}