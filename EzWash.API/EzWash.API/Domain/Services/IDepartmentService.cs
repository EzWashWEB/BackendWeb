using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Services.Communications;

namespace EzWash.API.Domain.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> ListAsync();
        Task<DepartmentResponse> GetByIdAsync(int id);
        Task<DepartmentResponse> SaveAsync(Department department);
        Task<DepartmentResponse> UpdateAsync(int id, Department department);
        Task<DepartmentResponse> DeleteAsync(int id);
    }
}