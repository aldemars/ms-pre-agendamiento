using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Service;

namespace ms_pre_agendamiento.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class HealthCareFacilitiesController
    {
        private readonly IHealthcareFacilityService _healthcareFacilityService;
        private readonly ICalendarAvailabilityService _calendarAvailabilityService;

        public HealthCareFacilitiesController(IHealthcareFacilityService healthCareFacilityService,
            ICalendarAvailabilityService calendarAvailabilityService)
        {
            _healthcareFacilityService =
                healthCareFacilityService ?? throw new ArgumentNullException("healthCareFacilityService");
            _calendarAvailabilityService =
                calendarAvailabilityService ?? throw new ArgumentNullException("CalendarAvailabilityService");
        }

        private IEnumerable<TimeSlot> GetAvailableSlotsFromService()
        {
            return _calendarAvailabilityService.GetAvailableBlocks();
        }

        [HttpGet]
        public IActionResult GetHealthCareFacilities()
        {
            var healthCareFacilities =
                _healthcareFacilityService.GetAll().Result.ToList();

            var availableBlocks = GetAvailableSlotsFromService();

            foreach (var facility in healthCareFacilities)
            {
                facility.disponibilidad = availableBlocks;
            }

            var centers = new Dictionary<string, List<HealthcareFacility>>();
            centers.Add("centros", healthCareFacilities);

            return new ObjectResult(centers);
        }
    }
}