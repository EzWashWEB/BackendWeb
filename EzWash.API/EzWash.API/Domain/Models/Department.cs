namespace EzWash.API.Domain.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        //one to many con province
        //TODO: Implementar Province
        //public IList<Province> Provinces { get; set; } = new List<Province>();
    }
}