using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Models.Request;

namespace ms_pre_agendamiento.Controllers
{
    [ApiController, Route("[controller]")]
    public class CalendarAvailabilityController  : ControllerBase
    {
        private readonly ICalendarAvailabilityService _calendarAvailabilityService;

        public CalendarAvailabilityController(ICalendarAvailabilityService calendarAvailabilityService)
        {
            _calendarAvailabilityService =
                calendarAvailabilityService ?? throw new ArgumentNullException("CalendarAvailabilityService");
        }

        public IEnumerable<TimeSlot> GetAvailableSlotsFromService()
        {
            return _calendarAvailabilityService.GetAvailableBlocks();
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAvailableSlots()
        {
            var availableBlocks = _calendarAvailabilityService.GetAvailableBlocks();
            if (!availableBlocks.Any())
            {
                return new NoContentResult();
            }

            return new ObjectResult(availableBlocks);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCalendarWithAvailableTimeSlots([FromBody] CalendarRequest calendarRequest)
        {
            return Ok(new Calendar(
                calendarRequest.From,
                calendarRequest.To,
                GetAvailableSlotsFromService().ToList()
            ));
        }
    }
}