using Aruba.Eis.Models.Bl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aruba.Eis.Models.Views
{
    public class TeamIndexViewModel
    {
        [Display(Name = "FILTER", ResourceType = typeof(I18nResource))]
        public string Filter { get; set; }

        public IList<Team> TeamList;

        public TeamIndexViewModel()
        {
            TeamList = new List<Team>();
        }
    }
}