using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aruba.Eis.Models.Bl
{
    public class ActivityResource
    {
        public int Id { get; set; }

        [Required]
        public string RoleId { get; set; }

        public int MinOccurs { get; set; }

        public int MaxOccurs { get; set; }
    }
}