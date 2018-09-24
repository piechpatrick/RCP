using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Repositories
{
    public interface IReadOnlyRepository<TItem>
    {
        TItem Get(Func<TItem, bool> predicate);
        IEnumerable<TItem> GetAll(Func<TItem, bool> predicate);
        IEnumerable<TItem> GetAll();
        Task<IEnumerable<TItem>> GetAllAsync();
    }
}
