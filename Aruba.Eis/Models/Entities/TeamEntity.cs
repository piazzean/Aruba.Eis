using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aruba.Eis.Models.Entities
{
    [Table("Teams")]
    public partial class TeamEntity : BaseEntity
    {
        public TeamEntity()
        {
            this.Resources = new HashSet<TeamResourceEntity>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public virtual ICollection<TeamResourceEntity> Resources { get; set; }
    }
}