using System.Collections.Generic;
using System.Threading.Tasks;
using Ms_pre_agendamiento.Models;

namespace Ms_pre_agendamiento.Service
{
    public interface IHealthCareFacilityService
    {
        Task<IEnumerable<HealthcareFacility>> GetAll();
    }
}