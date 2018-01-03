using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aruba.Eis.Models.Entities
{
    [Table("ActivityResources")]
    public partial class ActivityResourceEntity : BaseEntity
    {
        public int Id { get; set; }

        public int ActivityId { get; set; }

        public ActivityEntity Activity { get; set; }

        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual IdentityRole Role { get; set; }

        public int MinOccurs { get; set; }

        public int MaxOccurs { get; set; }
    }
}