using Aruba.Eis.Models.Entities;

namespace Aruba.Eis.Models.Bl
{
    public class Assignment
    {
        public int Id { get; set; }

        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        public string UserId { get; set; }

        public UserEntity User { get; set; }

        public string RoleId { get; set; }
    }
}