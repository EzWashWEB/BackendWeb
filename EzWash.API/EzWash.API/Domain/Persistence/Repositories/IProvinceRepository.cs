using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;

namespace EzWash.API.Domain.Persistence.Repositories
{
    public interface IProvinceRepository
    {
        Task<IEnumerable<Province>> ListByDepartmentId(int departmentId);
        Task<Province> FindById(int id);
        Task AddAsync(Province province);
        void Update(Province province);
        void Delete(Province province);
    }
}