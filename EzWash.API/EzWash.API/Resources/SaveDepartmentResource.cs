using System.ComponentModel.DataAnnotations;

namespace EzWash.API.Resources
{
    public class SaveDepartmentResource
    {
        //Data annotations: Reglas de validacion
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}