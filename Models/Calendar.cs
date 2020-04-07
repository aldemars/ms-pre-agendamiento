using System;
using System.Collections.Generic;

namespace ms_pre_agendamiento
{
    public class Calendar
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<ScheduledCalendarBlock> ScheduledBlocks { get; set; }
    }
}