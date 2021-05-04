﻿namespace EzWash.API.Domain.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        
        //one to many con locations
        //TODO: Implementar Location
        //public IList<Location> Locations { get; set; } = new List<Location>();
    }
}