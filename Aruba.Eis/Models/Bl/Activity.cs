using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aruba.Eis.Models.Bl
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<ActivityResource> Resources { get; set; }
    }
}