using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Repository;
using ms_pre_agendamiento.Service;

namespace ms_pre_agendamiento.Controllers
{
    [Authorize(Policy = "isScheduler")]
    [ApiController, Route("[controller]")]
    public class HealthCareFacilitiesController  : ControllerBase
    {
        private readonly IHealthCareFacilityService _healthcareFacilityService;
        private readonly ICalendarAvailabilityService _calendarAvailabilityService;
        private readonly IHealthCareFacilityRepository _healthCareFacilityRepository;

        public HealthCareFacilitiesController(IHealthCareFacilityService healthCareFacilityService,
            ICalendarAvailabilityService calendarAvailabilityService, IHealthCareFacilityRepository healthCareFacilityRepository)
        {
            _healthcareFacilityService =
                healthCareFacilityService ?? throw new ArgumentNullException("healthCareFacilityService");
            _calendarAvailabilityService =
                calendarAvailabilityService ?? throw new ArgumentNullException("CalendarAvailabilityService");
            _healthCareFacilityRepository = healthCareFacilityRepository;
        }

        [HttpGet(Name = "GetHealthCareFacility")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHealthCareFacilities()
        {
            var healthCareFacilities = _healthCareFacilityRepository.GetAllHealthCareFacilities();

            if (healthCareFacilities == null)
            {
                return NotFound();
            }

            return Ok(healthCareFacilities);
        }
        
        
    }
}