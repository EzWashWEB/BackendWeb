using EzWash.API.Domain.Models;
using EzWash.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Domain.Services
{
    public interface IWalletService
    {
        Task<IEnumerable<Wallet>> ListAsync();
        Task<WalletResponse> GetByIdAsync(int id);
        Task<WalletResponse> SaveAsync(Wallet wallet);
        Task<WalletResponse> UpdateAsync(int id, Wallet wallet);
        Task<WalletResponse> DeleteAsync(int id);
    }
}
