using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Domain.Models
{
    public class Wallet
    { 
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }

    }
}
