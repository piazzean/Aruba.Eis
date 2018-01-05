using System.ComponentModel.DataAnnotations;

namespace Aruba.Eis.Models.Bl
{
    public class ScheduleResource
    {
        public int Id { get; set; }

        [Required]
        public string RoleId { get; set; }

        public int MinOccurs { get; set; }

        public int MaxOccurs { get; set; }
    }
}