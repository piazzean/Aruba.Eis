using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aruba.Eis.Models.Entities
{
    [Table("ScheduleResources")]
    public partial class ScheduleResourceEntity : BaseEntity
    {
        public int Id { get; set; }

        public int ScheduleId { get; set; }

        public ScheduleEntity Schedule { get; set; }

        public string RoleId { get; set; }

        public int MinOccurs { get; set; }

        public int MaxOccurs { get; set; }
    }
}