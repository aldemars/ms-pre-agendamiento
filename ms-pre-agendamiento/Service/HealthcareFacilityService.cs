using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Service;
using static System.Text.Json.JsonSerializer;

namespace ms_pre_agendamiento
{
    public class HealthcareFacilityService : IHealthcareFacilityService
    {
        private readonly ILogger _logger;

        private readonly HttpClient _httpClient;

        public HealthcareFacilityService(IHttpClientFactory httpClientFactory,
            ILogger<HealthcareFacilityService> logger)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("HealthcareFacilitiesAPI");
        }

        public async Task<IEnumerable<HealthcareFacility>> GetAll()
        {
            IEnumerable<HealthcareFacility> centers = new List<HealthcareFacility>();
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