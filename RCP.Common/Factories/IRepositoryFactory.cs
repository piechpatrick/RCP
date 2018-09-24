using RCP.Common.Repositories;

namespace RCP.Common.Factories
{
    public interface IRepositoryFactory<TItem>
    {
        IRepository<TItem> CreateRepository();
    }
}
