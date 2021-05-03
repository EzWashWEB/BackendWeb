using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Domain.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //one to many con Benefit
        //TODO: implementar Benefit
        public IList<Benefit> Benefits { get; set; } = new List<Benefit>();
    }
}
