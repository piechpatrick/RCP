using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Repositories
{
    public abstract class RepositoryBase<TItem> : IRepository<TItem>
    {
        protected RepositoryBase()
        {
        }

        public bool TryAdd(TItem item)
        {
            return TryAddCore(item);
        }

        public bool TryAdd(IEnumerable<TItem> items)
        {
            if (items == null)
                throw new ArgumentNullException();

            return TryAddCore(items);
        }

        public bool TryRemove(TItem item)
        {
            return TryRemoveCore(item);
        }

        public bool TryRemove(IEnumerable<TItem> items)
        {
            if (items == null)
                throw new ArgumentNullException();

            return TryRemoveCore(items);
        }

        public TItem Get(Func<TItem, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException();

            return GetCore(predicate);
        }

        public IEnumerable<TItem> GetAll(Func<TItem, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException();

            return GetAllCore(predicate);
        }

        public IEnumerable<TItem> GetAll()
        {
            return GetAllCore();
        }

        public bool TrySet(IEnumerable<TItem> items)
        {
            if (items == null)
                throw new ArgumentNullException();

            return TrySetCore(items);
        }

        public Task<bool> TrySetAsync(IEnumerable<TItem> items)
        {
            if (items == null)
                throw new ArgumentNullException();

            return TrySetCoreAsync(items);
        }

        protected abstract bool TryAddCore(TItem item);
        protected abstract bool TryAddCore(IEnumerable<TItem> items);
        protected abstract bool TryRemoveCore(TItem item);
        protected abstract bool TryRemoveCore(IEnumerable<TItem> items);
        protected abstract TItem GetCore(Func<TItem, bool> predicate);
        protected abstract IEnumerable<TItem> GetAllCore(Func<TItem, bool> predicate);
        protected abstract IEnumerable<TItem> GetAllCore();
        protected abstract bool TrySetCore(IEnumerable<TItem> items);
        protected abstract Task<bool> TrySetCoreAsync(IEnumerable<TItem> items);

        public async Task<IEnumerable<TItem>> GetAllAsync()
        {
            return await Task.Factory.StartNew(() => { return this.GetAll(); });
        }
    }
}
