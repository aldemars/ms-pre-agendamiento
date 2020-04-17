using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Service;

namespace ms_pre_agendamiento.Controllers
{
    [ApiController, ApiExplorerSettings(IgnoreApi = true), Route("[controller]")]
    public class HealthCareFacilitiesController
    {
        private readonly IHealthCareFacilityService _healthcareFacilityService;
        private readonly ICalendarAvailabilityService _calendarAvailabilityService;

        public HealthCareFacilitiesController(IHealthCareFacilityService healthCareFacilityService,
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
            var availableBlocks = GetAvailableSlotsFromService();

            var healthCareFacilities =
                _healthcareFacilityService.GetAll().Result.ToList();

            foreach (var facility in healthCareFacilities)
            {
                facility.disponibilidad = availableBlocks;
            }

            const string keyCenterDictionary = "centros";
            var centers = new Dictionary<string, List<HealthcareFacility>>
            {
                {keyCenterDictionary, healthCareFacilities}
            };

            return new ObjectResult(centers);
        }
    }
}