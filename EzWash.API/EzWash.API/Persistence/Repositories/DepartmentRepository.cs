using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Contexts;
using EzWash.API.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EzWash.API.Persistence.Repositories
{
    public class DepartmentRepository: BaseRepository, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Department>> ListAsync()
        {
            return await _context.Deparments.ToListAsync();
        }

        public async Task AddAsync(Department department)
        {
            await _context.Deparments.AddAsync(department);
        }

        public async Task<Department> FindById(int id)
        {
            return await _context.Deparments.FindAsync(id);
        }

        public void Update(Department department)
        {
            _context.Deparments.Update(department);
        }

        public void Remove(Department department)
        {
            _context.Deparments.Remove(department);
        }
    }
}