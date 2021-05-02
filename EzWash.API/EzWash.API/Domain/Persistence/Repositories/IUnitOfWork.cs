using System.Threading.Tasks;

namespace EzWash.API.Domain.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}