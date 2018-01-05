using Aruba.Eis.Models.Bl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aruba.Eis.Models.Views
{
    public class ScheduleCreateViewModel
    {
        [Required]
        public string Activity { get; set; }
    }
}