using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aruba.Eis.Models.Bl
{
    public class ActivityResource
    {
        public int Id { get; set; }

        public string RoleId { get; set; }

        public Role Role { get; set; }

        public int MinOccurs { get; set; }

        public int MaxOccors { get; set; }
    }
}