using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<int> Get()
        {
            return new[]
            {
                calendarAvailabilityService.GetBlocksSize()
            };
        }
        
    }
}