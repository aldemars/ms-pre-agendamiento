using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ms_pre_agendamiento.Models
{
    public class HealthcareFacility
    {
        public string id { get; set; }
        public string nombre { set; get; }
        public IEnumerable<TimeSlot> disponibilidad { set; get; }
    }
}