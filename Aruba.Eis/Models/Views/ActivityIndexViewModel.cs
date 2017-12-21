using Aruba.Eis.Models.Bl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aruba.Eis.Models.Views
{
    public class ActivityIndexViewModel
    {
        [Display(Name = "FILTER", ResourceType = typeof(I18nResource))]
        public string Filter { get; set; }

        public IList<Activity> ActivityList;

        public ActivityIndexViewModel()
        {
            ActivityList = new List<Activity>();
        }
    }
}