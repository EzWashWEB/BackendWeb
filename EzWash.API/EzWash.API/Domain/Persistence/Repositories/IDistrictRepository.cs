using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;

namespace EzWash.API.Domain.Persistence.Repositories
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<District>> ListByProvinceId(int provinceId);
        Task<District> FindById(int id);
        Task AddAsync(District district);
        void Update(District district);
        void Delete(District district);
    }
}