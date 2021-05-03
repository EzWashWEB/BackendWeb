using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Services.Communications;

namespace EzWash.API.Domain.Services
{
    public interface IProvinceService
    {
        Task<IEnumerable<Province>> ListByDepartmentIdAsync(int departmentId);
        Task<ProvinceResponse> GetByDepartmentIdAndIdAsync(int departmentId, int id);
        Task<ProvinceResponse> SaveAsync(int departmentId, Province province);
        Task<ProvinceResponse> UpdateAsync(int departmentId, int id, Province province);
        Task<ProvinceResponse> DeleteAsync(int departmentId, int id);
    }
}