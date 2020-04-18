namespace Ms_pre_agendamiento.Models
{
    using System.Collections.Generic;

    public class HealthcareFacility
    {
        public string id { get; set; }

        public string nombre { get; set; }

        public IEnumerable<TimeSlot> disponibilidad { get; set; }
    }
}