using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Repositories
{
    public interface ISavableRepository<TItem> : IRepository<TItem>
    {
        bool TrySave();
    }
}
