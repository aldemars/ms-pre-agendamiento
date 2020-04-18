using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ms_pre_agendamiento.Models;
using Ms_pre_agendamiento.Service;

namespace Ms_pre_agendamiento.Controllers
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

        [HttpGet]
        public IActionResult GetHealthCareFacilities()
        {
            const string keyCenterDictionary = "centros";
            var availableBlocks = GetAvailableSlotsFromService() ??
                                  throw new ArgumentNullException("GetAvailableSlotsFromService()");

            var healthCareFacilities =
                _healthcareFacilityService.GetAll().Result.ToList();

            foreach (var facility in healthCareFacilities)
            {
                facility.disponibilidad = availableBlocks;
            }

            var centers = new Dictionary<string, List<HealthcareFacility>>
            {
                { keyCenterDictionary, healthCareFacilities },
            };

            return new ObjectResult(centers);
        }
        
        private IEnumerable<TimeSlot> GetAvailableSlotsFromService() =>
            _calendarAvailabilityService.GetAvailableBlocks();
    }
}