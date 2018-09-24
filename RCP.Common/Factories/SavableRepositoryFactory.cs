using RCP.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Factories
{
    public class SavableRepositoryFactory<TItem> : ISavableRepositoryFactory<TItem>
    {
        public SavableRepositoryFactory(ISavableRepository<TItem> source)
        {
            this.Source = source;
        }

        public ISavableRepository<TItem> CreateRepository()
        {
            return this.Source;
        }

        public ISavableRepository<TItem> Source { get; private set; }
    }
}
