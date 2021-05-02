using AutoMapper;
using EzWash.API.Domain.Models;
using EzWash.API.Resources;

namespace EzWash.API.Mapping
{
    public class ModelToResourceProfile: Profile
    {
        public ModelToResourceProfile()
        {
            //TODO: Create Model to Resource Mapping
            
            CreateMap<Department, DepartmentResource>();
            
        }
    }
}