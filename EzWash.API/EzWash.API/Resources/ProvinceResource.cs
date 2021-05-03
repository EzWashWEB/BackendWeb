namespace EzWash.API.Resources
{
    public class ProvinceResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DepartmentResource Department { get; set; }
    }
}