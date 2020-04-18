using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ms_pre_agendamiento.Models;
using static System.Text.Json.JsonSerializer;

namespace Ms_pre_agendamiento.Service.Impl
{
    public class HealthCareFacilityService : IHealthCareFacilityService
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public HealthCareFacilityService(IHttpClientFactory httpClientFactory,
            ILogger<HealthCareFacilityService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClientFactory.CreateClient("HealthCareFacilitiesAPI");
        }

        public async Task<IEnumerable<HealthcareFacility>> GetAll()
        {
            var healthCareFacilityAPIResponse = await _httpClient.GetAsync($"/api/centros/");
            if (!healthCareFacilityAPIResponse.IsSuccessStatusCode)
            {
                _logger.LogWarning("Error retrieving HealthCare Facilities, Status Code:{StatusCode}",
                    healthCareFacilityAPIResponse.StatusCode);
                throw new HttpRequestException(healthCareFacilityAPIResponse.ReasonPhrase);
            }

            var healthCareFacilityContent = await healthCareFacilityAPIResponse.Content.ReadAsStringAsync();
            var healthCareFacilityData =
                Deserialize<Dictionary<string, List<HealthcareFacility>>>(healthCareFacilityContent);
            return healthCareFacilityData["centros"];
        }
    }
}