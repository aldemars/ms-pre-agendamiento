using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ms_pre_agendamiento.Models;
using Ms_pre_agendamiento.Models.Request;

namespace Ms_pre_agendamiento.Controllers
{
    [ApiController, ApiExplorerSettings(IgnoreApi = true), Route("[controller]")]
    public class CalendarAvailabilityController
    {
        private readonly ICalendarAvailabilityService _calendarAvailabilityService;

        public CalendarAvailabilityController(ICalendarAvailabilityService calendarAvailabilityService)
        {
            _calendarAvailabilityService =
                calendarAvailabilityService ?? throw new ArgumentNullException("CalendarAvailabilityService");
        }

        public IEnumerable<TimeSlot> AvailableSlotsFromService => _calendarAvailabilityService.GetAvailableBlocks();

        [HttpGet]
        public IActionResult GetAvailableSlots()
        {
            var availableBlocks = _calendarAvailabilityService.GetAvailableBlocks() ??
                                  throw new ArgumentNullException("_calendarAvailabilityService.GetAvailableBlocks()");
            if (!availableBlocks.Any())
            {
                return new NoContentResult();
            }

            return new ObjectResult(availableBlocks);
        }

        [HttpPost]
        public Calendar GetCalendarWithAvailableTimeSlots([FromBody] CalendarRequest calendarRequest) =>
            new Calendar(
                calendarRequest.From,
                calendarRequest.To,
                AvailableSlotsFromService.ToList());
    }
}
