using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aruba.Eis.Models.Entities
{
    [Table("Activities")]
    public partial class ActivityEntity : BaseEntity
    {
        public ActivityEntity()
        {
            this.Resources = new HashSet<ActivityResourceEntity>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public virtual ICollection<ActivityResourceEntity> Resources { get; set; }
    }
}