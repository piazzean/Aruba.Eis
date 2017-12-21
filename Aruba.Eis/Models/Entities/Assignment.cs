using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aruba.Eis.Models.Entities
{
    [Table("Assignements")]
    public partial class AssignmentEntity : BaseEntity
    {
        public int Id { get; set; }

        public int ScheduleId { get; set; }

        public ScheduleEntity Schedule { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public IdentityRole Role { get; set; }
    }
}