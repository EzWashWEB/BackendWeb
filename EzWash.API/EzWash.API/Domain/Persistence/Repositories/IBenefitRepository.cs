using EzWash.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Domain.Persistence.Repositories
{
    public interface IBenefitRepository
    {
        Task<IEnumerable<Benefit>> ListAsync();
        Task AddAsync(Benefit benefit);
        Task<Benefit> FindById(int id);
        void Update(Benefit benefit);
        void Remove(Benefit benefit);
    }
}
