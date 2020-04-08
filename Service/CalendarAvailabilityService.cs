using System;
using System.Collections.Generic;
using System.Linq;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento
{
    public class CalendarAvailabilityService : ICalendarAvailabilityService
    {
        private readonly IBusyCalendarTimeSlotsRepository _busyCalendarTimeSlotsRepository;
        private readonly IAllCalendarTimeSlotsRepository _allCalendarTimeSlotsRepository;

        public CalendarAvailabilityService(IBusyCalendarTimeSlotsRepository busyCalendarTimeSlotsRepository,
            IAllCalendarTimeSlotsRepository allCalendarTimeSlotsRepository)
        {
            _busyCalendarTimeSlotsRepository =
                busyCalendarTimeSlotsRepository ?? throw new ArgumentNullException("ScheduledCalendarBlockRepository");
            _allCalendarTimeSlotsRepository =
                allCalendarTimeSlotsRepository ?? throw new ArgumentNullException("AllCalendarBlockRepository");
        }

        public IEnumerable<TimeSlot> GetAvailableBlocks()
        {
            var allSlots = _allCalendarTimeSlotsRepository.GetAllTimeSlotsMock();
            var busySlots = _busyCalendarTimeSlotsRepository.GetScheduledBlocksMock();
            return allSlots.Except(busySlots, new TimeSlotComparator());
        }
    }
}