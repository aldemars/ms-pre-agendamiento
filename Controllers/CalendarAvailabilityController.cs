using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarAvailabilityController
    {
        private readonly ICalendarAvailabilityService calendarAvailabilityService;

        public CalendarAvailabilityController(ICalendarAvailabilityService calendarAvailabilityService)
        {
            this.calendarAvailabilityService = 
                calendarAvailabilityService ?? throw new ArgumentNullException("CalendarAvailabilityService");
            
        }
        
        [HttpGet]
        public IEnumerable<TimeSlot> Get()
        {
            return calendarAvailabilityService.GetAvailableBlocks();
        }
        
    }
}