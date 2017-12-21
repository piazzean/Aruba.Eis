using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
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

        [ForeignKey("RoleId")]
        public IdentityRole Role { get; set; }

        public int MinOccurs { get; set; }

        public int MaxOccors { get; set; }
    }
}