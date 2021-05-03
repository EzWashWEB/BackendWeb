using EzWash.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Domain.Services.Communications
{
    public class BenefitResponse : BaseResponse<Benefit>
    {


        public BenefitResponse(string message) : base(message)
        {
        }

        public BenefitResponse(Benefit resource) : base(resource)
        {
        }
    }
}
