using AutoMapper;
using EzWash.API.Domain.Models;
using EzWash.API.Resources;

namespace EzWash.API.Mapping
{
    public class ResourceToModelProfile: Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveDepartmentResource, Department>();

            CreateMap<SaveWalletResource, Wallet>();

            CreateMap<SavePlanResource, Plan>();

            CreateMap<SaveBenefitResource, Benefit>();

        }
    }
}