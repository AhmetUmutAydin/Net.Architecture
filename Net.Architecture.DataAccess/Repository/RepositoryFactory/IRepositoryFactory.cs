using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.DataAccess.Repository.RepositoryFactory
{
    public interface IRepositoryFactory
    {
        IBaseRepository<T> CreateBaseRepository<T>() where T : class, IEntity, new();
        T CreateCustomRepository<T>() where T : class;
    }
}
