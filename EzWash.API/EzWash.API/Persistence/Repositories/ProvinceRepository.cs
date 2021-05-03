using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Contexts;
using EzWash.API.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EzWash.API.Persistence.Repositories
{
    public class ProvinceRepository: BaseRepository, IProvinceRepository
    {
        public ProvinceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Province>> ListByDepartmentId(int departmentId)
        {
            return await _context.Provinces
                .Where(p => p.DepartmentId == departmentId)
                .Include(p => p.Department)
                .ToListAsync();
        }

        public async Task<Province> FindById(int id)
        {
            return await _context.Provinces.FindAsync(id);
        }

        public async Task AddAsync(Province province)
        {
            await _context.Provinces.AddAsync(province);
        }

        public void Update(Province province)
        {
            _context.Provinces.Update(province);
        }

        public void Delete(Province province)
        {
            _context.Provinces.Remove(province);
        }
    }
}