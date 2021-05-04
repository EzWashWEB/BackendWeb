using EzWash.API.Domain.Models;

namespace EzWash.API.Domain.Services.Communications
{
    public class DistrictResponse: BaseResponse<District>
    {
        public DistrictResponse(District resource) : base(resource)
        {
        }

        public DistrictResponse(string message) : base(message)
        {
        }
    }
}