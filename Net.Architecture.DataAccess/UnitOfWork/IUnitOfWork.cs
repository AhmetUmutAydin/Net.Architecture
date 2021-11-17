using System.Threading.Tasks;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.DataAccess.Repository.RepositoryFactory;
using Net.Architecture.Entities.BaseEntities;

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
        IBaseRepository<T> Repository<T>() where T : class, IEntity, new();
        T CustomRepository<T>() where T : class;
    }
}