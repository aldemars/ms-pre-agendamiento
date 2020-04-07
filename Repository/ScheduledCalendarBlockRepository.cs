using System;
using System.Collections.Generic;

namespace ms_pre_agendamiento
{
    public class ScheduledCalendarBlockRepository
    {
        public List<ScheduledCalendarBlock> GetScheduledBlocks() {
            List<ScheduledCalendarBlock> ScheduledBlocks = new List<ScheduledCalendarBlock>();
            
            ScheduledBlocks.Add(
                new ScheduledCalendarBlock() {
                    Date = DateTime.Now,
                    From = 13,
                    To = 14
                }
            );

            return ScheduledBlocks;
        }

    }
}