using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Resources
{
    public class SaveWalletResource
    {
        [Required]
        public double Amount { get; set; }

        [Required]
        [MaxLength(30)]
        public string Currency { get; set; }
    }
}
