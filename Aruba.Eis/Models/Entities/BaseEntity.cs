using System;
using System.ComponentModel.DataAnnotations;

namespace Aruba.Eis.Models.Entities
{
    public class BaseEntity
    {
        public DateTime? DateCreated { get; set; }

        [MaxLength(50)]
        public string UserCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [MaxLength(50)]
        public string UserModified { get; set; }
    }
}