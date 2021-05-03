using EzWash.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Domain.Persistence.Repositories
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> ListAsync();
        Task AddAsync(Wallet wallet);
        Task<Wallet> FindById(int id);
        void Update(Wallet wallet);
        void Remove(Wallet wallet);
    }
}
