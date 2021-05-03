namespace EzWash.API.Domain.Models
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        
        //one to many con district
        //TODO: Implementar District
        //public IList<District> Districts { get; set; } = new List<District>();
    }
}