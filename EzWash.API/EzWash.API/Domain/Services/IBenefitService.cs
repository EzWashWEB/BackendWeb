using EzWash.API.Domain.Models;
using EzWash.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Domain.Services
{
    
        public interface IBenefitService
        {
            Task<IEnumerable<Benefit>> ListAsync();
            Task<BenefitResponse> GetByIdAsync(int id);
            Task<BenefitResponse> SaveAsync(Benefit benefit);
            Task<BenefitResponse> UpdateAsync(int id, Benefit benefit);
            Task<BenefitResponse> DeleteAsync(int id);
        }

    
}
