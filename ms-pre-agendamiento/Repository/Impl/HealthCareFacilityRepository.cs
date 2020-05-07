using System.Collections.Generic;
using System.Linq;
using Dapper;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Repository.Impl
{
    public class HealthCareFacilityRepository : IHealthCareFacilityRepository
    {
        private IRepositoryCommandExecuter _command;

        public HealthCareFacilityRepository(IRepositoryCommandExecuter command)
        {
            _command = command;
        }

        public HealthCareFacility GetById(int id)
        {
            var healthCareFacility = _command.ExecuteCommand(connection =>
                connection.Query<HealthCareFacility>(HealthCareFacilityCommand.GetById, new {@Id = id})
                    .SingleOrDefault());
            return healthCareFacility;
        }

        public List<HealthCareFacility> GetAllHealthCareFacilities()
        {
            var healthCareFacilities = _command.ExecuteCommand(connection =>
                connection.Query<HealthCareFacility>(HealthCareFacilityCommand.GetAllHealthCareFacilities)
                    .ToList());
            return healthCareFacilities;
        }

        public class HealthCareFacilityCommand
        {
            public static string GetById => "Select * From healthcare_facility where Id= @Id";

            public static string GetAllHealthCareFacilities =>
                "Select * From healthcare_facility";
        }
    }
}