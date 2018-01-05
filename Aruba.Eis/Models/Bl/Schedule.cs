using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aruba.Eis.Models.Bl
{
    public class Schedule
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime EndDateTime { get; set; }

        public IList<ScheduleResource> Resources { get; set; }

        public IList<Assignment> Assignments { get; set; }
    }
}