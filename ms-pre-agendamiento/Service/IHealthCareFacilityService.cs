using System.Collections.Generic;
using System.Threading.Tasks;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Service
{
    public interface IHealthCareFacilityService
    {
        Task<IEnumerable<HealthcareFacility>> GetAll();
    }
}