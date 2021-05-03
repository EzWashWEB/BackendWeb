using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Contexts;
using EzWash.API.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Persistence.Repositories
{
    public class WalletRepository : BaseRepository, IWalletRepository
    {
        public WalletRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
        }

        public async Task<Wallet> FindById(int id)
        {
            return await _context.Wallets.FindAsync(id);
        }

        public async Task<IEnumerable<Wallet>> ListAsync()
        {
            return await _context.Wallets.ToListAsync();
        }

        public void Remove(Wallet wallet)
        {
            _context.Wallets.Remove(wallet);
        }

        public void Update(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
        }
    }
}
