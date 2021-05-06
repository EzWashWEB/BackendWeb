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
    public class BenefitRepository
    {
         public class PlanRepository : BaseRepository, IBenefitRepository
        {
            public PlanRepository(AppDbContext context) : base(context)
            {
            }

            public async Task AddAsync(Benefit benefit)
            {
                await _context.Benefits.AddAsync(benefit);
            }

            public async Task<Benefit> FindById(int id)
            {
                return await _context.Benefits.FindAsync(id);
            }

            public async Task<IEnumerable<Benefit>> ListAsync()
            {
                return await _context.Benefits.ToListAsync();
            }

            public void Remove(Benefit benefit)
            {
                _context.Benefits.Remove(benefit);
            }

            public void Update(Benefit benefit)
            {
                _context.Benefits.Update(benefit);
            }
        }
    }
}
