using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Aruba.Eis.Models.Bl
{
    public class Schedule
    {
        private DateTime _startDateTime;

        private DateTime _endDateTime;

        private readonly string _dateFormat;
        
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime StartDateTime 
        { 
            get => _startDateTime;
            set => _startDateTime = value;
        }

        public string StartDateString
        {
            get => _startDateTime.ToString(_dateFormat); 
            set => _startDateTime = DateTime.ParseExact(value, _dateFormat, CultureInfo.InvariantCulture); 
        }

        public DateTime EndDateTime 
        { 
            get => _endDateTime;
            set => _endDateTime = value;
        }

        public string EndDateString
        {
            get => _endDateTime.ToString(_dateFormat); 
            set => _endDateTime = DateTime.ParseExact(value, _dateFormat, CultureInfo.InvariantCulture);  
        }        

        public IList<ScheduleResource> Resources { get; set; }

        public IList<Assignment> Assignments { get; set; }

        public Schedule()
        {
            _dateFormat = "dd/MM/yyyy HH:mm";
        }
    }
}