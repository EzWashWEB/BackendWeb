using EzWash.API.Domain.Persistence.Contexts;

namespace EzWash.API.Persistence.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}