namespace EzWash.API.Resources
{
    public class DistrictResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProvinceResource Province { get; set; }
    }
}