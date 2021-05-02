using System.Threading.Tasks;
using EzWash.API.Domain.Persistence.Contexts;
using EzWash.API.Domain.Persistence.Repositories;

namespace EzWash.API.Persistence.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}