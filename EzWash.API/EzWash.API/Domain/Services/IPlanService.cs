using EzWash.API.Domain.Models;
using EzWash.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Domain.Services
{
    public interface IPlanService
    {
        Task<IEnumerable<Plan>> ListAsync();
        Task<PlanResponse> GetByIdAsync(int id);
        Task<PlanResponse> SaveAsync(Plan plan);
        Task<PlanResponse> UpdateAsync(int id, Plan plan);
        Task<PlanResponse> DeleteAsync(int id);
    }
}
