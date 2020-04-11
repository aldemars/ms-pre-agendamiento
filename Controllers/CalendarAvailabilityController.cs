using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarAvailabilityController
    {
        private readonly ICalendarAvailabilityService _calendarAvailabilityService;

        public CalendarAvailabilityController(ICalendarAvailabilityService calendarAvailabilityService)
        {
            _calendarAvailabilityService =
                calendarAvailabilityService ?? throw new ArgumentNullException("CalendarAvailabilityService");
        }

        [HttpGet]
        public IActionResult GetAvailableSlots()
        {
            IEnumerable<TimeSlot> availableBlocks = _calendarAvailabilityService.GetAvailableBlocks();
            if (!availableBlocks.Any())
            {
                return new NoContentResult();
            }
            return new ObjectResult(availableBlocks);
        }
    }
}