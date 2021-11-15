using System.Threading.Tasks;
using Net.Architecture.DataAccess.Repository.RepositoryFactory;

namespace Net.Architecture.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IRepositoryFactory
    {
        Task<int> ForceSaveChangesAsync();
        Task<int> SaveChangesAsync();
        Task RollbackAsync();
        Task CommitAsync();
        Task CheckTransactionAsync();
        Task CreateTransactionAsync();
        T Repository<T>() where T : class;
    }
}