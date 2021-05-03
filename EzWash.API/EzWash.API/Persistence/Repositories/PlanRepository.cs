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
    public class PlanRepository : BaseRepository, IPlanRepository
    {
        public PlanRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Plan plan)
        {
            await _context.Plans.AddAsync(plan);
        }

        public async Task<Plan> FindById(int id)
        {
            return await _context.Plans.FindAsync(id);
        }

        public async Task<IEnumerable<Plan>> ListAsync()
        {
            return await _context.Plans.ToListAsync();
        }

        public void Remove(Plan plan)
        {
            _context.Plans.Remove(plan);
        }

        public void Update(Plan plan)
        {
            _context.Plans.Update(plan);
        }
    }
}
