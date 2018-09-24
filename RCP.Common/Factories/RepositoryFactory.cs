
using RCP.Common.Repositories;

namespace RCP.Common.Factories
{
    public class RepositoryFactory<TItem> : IRepositoryFactory<TItem>
    {
        public RepositoryFactory(IRepository<TItem> source)
        {
            this.Source = source;
        }

        public IRepository<TItem> CreateRepository()
        {
            return this.Source;
        }

        public IRepository<TItem> Source { get; private set; }
    }
}
