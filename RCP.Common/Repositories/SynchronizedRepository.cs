using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Repositories
{
    public abstract class SynchronizedRepository<TItem> : ISavableRepository<TItem>
    {
        protected SynchronizedRepository()
        {
            this.SyncRoot = new object();
        }

        public object SyncRoot { get; }

        public bool TryAdd(TItem item)
        {
            lock (this.SyncRoot)
            {
                return this.TryAddCore(item);
            }
        }

        public bool TryAdd(IEnumerable<TItem> items)
        {
            if (items == null)
                throw new ArgumentNullException();

            lock (this.SyncRoot)
            {
                return this.TryAddCore(items);
            }
        }

        public bool TryRemove(TItem item)
        {
            lock (this.SyncRoot)
            {
                return this.TryRemoveCore(item);
            }
        }

        public bool TryRemove(IEnumerable<TItem> items)
        {
            if (items == null)
                throw new ArgumentNullException();

            lock (this.SyncRoot)
            {
                return this.TryRemoveCore(items);
            }
        }

        public TItem Get(Func<TItem, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException();

            lock (this.SyncRoot)
            {
                return this.GetCore(predicate);
            }
        }

        public IEnumerable<TItem> GetAll(Func<TItem, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException();

            lock (this.SyncRoot)
            {
                return this.GetAllCore(predicate);
            }
        }

        public IEnumerable<TItem> GetAll()
        {
            lock (this.SyncRoot)
            {
                return this.GetAllCore();
            }
        }

        public bool TrySet(IEnumerable<TItem> items)
        {
            if (items == null)
                throw new ArgumentNullException();

            lock (this.SyncRoot)
            {
                return this.TrySetCore(items);
            }
        }

        public Task<bool> TrySetAsync(IEnumerable<TItem> items)
        {
            if (items == null)
                throw new ArgumentNullException();

            return this.TrySetCoreAsync(items);
        }

        protected virtual bool TryAddCore(TItem item)
        {
            throw new NotSupportedException();
        }

        protected virtual bool TryAddCore(IEnumerable<TItem> items)
        {
            throw new NotSupportedException();
        }

        protected virtual bool TryRemoveCore(TItem item)
        {
            throw new NotSupportedException();
        }

        protected virtual bool TryRemoveCore(IEnumerable<TItem> items)
        {
            throw new NotSupportedException();
        }

        protected virtual TItem GetCore(Func<TItem, bool> predicate)
        {
            throw new NotSupportedException();
        }

        protected virtual IEnumerable<TItem> GetAllCore(Func<TItem, bool> predicate)
        {
            throw new NotSupportedException();
        }

        protected virtual IEnumerable<TItem> GetAllCore()
        {
            throw new NotSupportedException();
        }

        protected virtual bool TrySetCore(IEnumerable<TItem> items)
        {
            throw new NotSupportedException();
        }

        public Task<bool> TrySetCoreAsync(IEnumerable<TItem> items)
        {
            throw new NotSupportedException();
        }


        public async Task<IEnumerable<TItem>> GetAllAsync()
        {
            return await Task.Factory.StartNew(() => { return this.GetAll(); });
        }

        public bool TrySave()
        {
            return true;
        }
    }
}
