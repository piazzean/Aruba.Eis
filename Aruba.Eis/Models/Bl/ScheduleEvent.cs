﻿namespace Aruba.Eis.Models.Bl
{
    public class ScheduleEvent
    {
        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string color { get; set; }
        public bool allDay { get; set; }
    }
}