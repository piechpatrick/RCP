using RCP.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Factories
{
    public interface ISavableRepositoryFactory<TItem>
    {
        ISavableRepository<TItem> CreateRepository();
    }
}
