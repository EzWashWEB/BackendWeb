using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Contexts;
using EzWash.API.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EzWash.API.Persistence.Repositories
{
    public class DistrictRepository: BaseRepository, IDistrictRepository
    {
        public DistrictRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<District>> ListByProvinceId(int provinceId)
        {
            return await _context.Districts
                .Where(p => p.ProvinceId == provinceId)
                .Include(p => p.Province)
                .Include(p=>p.Province.Department)
                .ToListAsync();
        }

        public async Task<District> FindById(int id)
        {
            return await _context.Districts.FindAsync(id);
        }

        public async Task AddAsync(District district)
        {
            await _context.Districts.AddAsync(district);
        }

        public void Update(District district)
        {
            _context.Districts.Update(district);
        }

        public void Delete(District district)
        {
            _context.Districts.Remove(district);
        }
    }
}