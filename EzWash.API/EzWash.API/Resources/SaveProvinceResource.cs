using System.ComponentModel.DataAnnotations;

namespace EzWash.API.Resources
{
    public class SaveProvinceResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}