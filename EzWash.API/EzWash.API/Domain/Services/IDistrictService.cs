using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Services.Communications;

namespace EzWash.API.Domain.Services
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> ListByProvinceIdAsync(int provinceId);
        Task<DistrictResponse> GetByDepartmentIdAndProvinceIdAndId(int departmentId, int provinceId, int id);
        Task<DistrictResponse> SaveAsync(int departmentId, int provinceId, District district);
        Task<DistrictResponse> UpdateAsync(int departmentId, int provinceId, int id, District district);
        Task<DistrictResponse> DeleteAsync(int departmentId, int provinceId, int id);
    }
}