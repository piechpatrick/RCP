using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Repositories
{
    public abstract class SavableRepositoryBase<TItem> : ISavableRepository<TItem>
    {
        protected SavableRepositoryBase()
        {

        }

        protected SavableRepositoryBase(IRepository<TItem> source)
        {
            this.Source = source;
        }

        public IRepository<TItem> Source { get; protected set; }


        public TItem Get(Func<TItem, bool> predicate)
        {
            return this.Source.Get(predicate);
        }

        public IEnumerable<TItem> GetAll(Func<TItem, bool> predicate)
        {
            return this.Source.GetAll(predicate);
        }

        public IEnumerable<TItem> GetAll()
        {
            return this.Source.GetAll();
        }

        public Task<IEnumerable<TItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool TryAdd(TItem item)
        {
            return this.Source.TryAdd(item);
        }

        public bool TryAdd(IEnumerable<TItem> items)
        {
            return this.Source.TryAdd(items);
        }

        public bool TryRemove(TItem item)
        {
            throw new NotImplementedException();
        }

        public bool TryRemove(IEnumerable<TItem> items)
        {
            throw new NotImplementedException();
        }

        public bool TrySave()
        {
            return this.TrySet(this.Source.GetAll());
        }

        public bool TrySet(IEnumerable<TItem> items)
        {
            return this.Source.TrySet(items);
        }

        public Task<bool> TrySetAsync(IEnumerable<TItem> items)
        {
            throw new NotImplementedException();
        }
    }
}
