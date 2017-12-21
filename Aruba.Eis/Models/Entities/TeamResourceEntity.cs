using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aruba.Eis.Models.Entities
{
    [Table("TeamResources")]
    public partial class TeamResourceEntity : BaseEntity
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public TeamEntity Team { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}