﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aruba.Eis.Models.Bl
{
    public class Role
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}