using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aruba.Eis.Models.Entities
{
    [Table("Schedules")]
    public partial class ScheduleEntity : BaseEntity
    {
        public ScheduleEntity()
        {
            this.Resources = new HashSet<ScheduleResourceEntity>();
            this.Assignments = new HashSet<AssignmentEntity>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public virtual ICollection<ScheduleResourceEntity> Resources { get; set; }

        public virtual ICollection<AssignmentEntity> Assignments { get; set; }
    }
}