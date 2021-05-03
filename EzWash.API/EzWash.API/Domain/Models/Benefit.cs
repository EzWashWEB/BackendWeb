using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Domain.Models
{
    public class Benefit
    {
        public int Id { get; set; }
        public string Description { get; set; }
        
        //Many to one with plans
    }
}
