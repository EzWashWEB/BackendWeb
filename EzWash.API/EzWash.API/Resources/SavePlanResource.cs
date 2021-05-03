﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Resources
{
    public class SavePlanResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
