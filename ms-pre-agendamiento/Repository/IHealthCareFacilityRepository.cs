using System.Collections.Generic;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Repository
{
    public interface IHealthCareFacilityRepository
    {
        HealthCareFacility GetById(int id);
        
        List<HealthCareFacility> GetAllHealthCareFacilities();

    }
}