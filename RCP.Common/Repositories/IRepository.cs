using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Repositories
{
    public interface IRepository<TItem> : IReadOnlyRepository<TItem>
    {
        bool TryAdd(TItem item);
        bool TryAdd(IEnumerable<TItem> items);
        bool TryRemove(TItem item);
        bool TryRemove(IEnumerable<TItem> items);
        bool TrySet(IEnumerable<TItem> items);
        Task<bool> TrySetAsync(IEnumerable<TItem> items);
    }
}
