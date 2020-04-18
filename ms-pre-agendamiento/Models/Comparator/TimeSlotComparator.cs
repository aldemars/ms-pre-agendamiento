namespace Ms_pre_agendamiento.Models.Comparator
{
    using System;
    using System.Collections.Generic;

    public class TimeSlotComparator : IEqualityComparer<TimeSlot>
    {
        public bool Equals(TimeSlot timeSlotA, TimeSlot timeSlotB)
        {
            if (timeSlotA == null || timeSlotB == null)
            {
                throw new ArgumentNullException(nameof(timeSlotA));
            }

            return timeSlotA.HourFrom == timeSlotB.HourFrom;
        }

        public int GetHashCode(TimeSlot timeSlot)
        {
            return timeSlot.HourFrom.GetHashCode();
        }
    }
}