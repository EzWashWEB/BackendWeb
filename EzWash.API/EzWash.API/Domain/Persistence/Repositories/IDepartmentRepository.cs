using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;

namespace EzWash.API.Domain.Persistence.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> ListAsync();
        Task AddAsync(Department department);
        Task<Department> FindById(int id);
        void Update(Department department);
        void Remove(Department department);
    }
}