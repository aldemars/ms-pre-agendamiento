using System.Collections.Generic;

namespace ms_pre_agendamiento.Models.Comparator
{
    public class TimeSlotComparator : IEqualityComparer<TimeSlot>
    {
        public bool Equals(TimeSlot timeSlotA, TimeSlot timeSlotB)
        {
            return timeSlotA.HourFrom == timeSlotB.HourFrom;
        }

        public int GetHashCode(TimeSlot timeSlot)
        {
            return timeSlot.HourFrom.GetHashCode();
        }
    }
}