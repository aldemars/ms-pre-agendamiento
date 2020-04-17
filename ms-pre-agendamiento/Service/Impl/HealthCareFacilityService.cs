using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ms_pre_agendamiento.Models;
using static System.Text.Json.JsonSerializer;

namespace ms_pre_agendamiento.Service.Impl
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
            var response = await _httpClient.GetAsync($"/api/centros/");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Error retrieving Healthcare Facilities, Status Code:{StatusCode}",
                    response.StatusCode);
                throw new HttpRequestException(response.ReasonPhrase);
            }

            var result = await response.Content.ReadAsStringAsync();
            var data = Deserialize<Dictionary<string, List<HealthcareFacility>>>(result);

            return data["centros"];
        }
    }
}