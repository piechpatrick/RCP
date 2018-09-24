using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCP.Common.Repositories
{
    public abstract class Container<TItem> : IRepository<TItem>
    {

        protected Container()
        {
            this.Items = new List<TItem>();
        }

        public List<TItem> Items { get; set; }

        public TItem Get(Func<TItem, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> GetAll(Func<TItem, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> GetAll()
        {
            return this.Items;
        }

        public async Task<IEnumerable<TItem>> GetAllAsync()
        {
            return await Task.Factory.StartNew(() => { return this.GetAll(); });
        }

        public bool TryAdd(TItem item)
        {
            throw new NotImplementedException();
        }

        public bool TryAdd(IEnumerable<TItem> items)
        {
            throw new NotImplementedException();
        }

        public bool TryRemove(TItem item)
        {
            throw new NotImplementedException();
        }

        public bool TryRemove(IEnumerable<TItem> items)
        {
            throw new NotImplementedException();
        }

        public bool TrySet(IEnumerable<TItem> items)
        {
            this.Items = items.ToList();
            return true;
        }

        public Task<bool> TrySetAsync(IEnumerable<TItem> items)
        {
            throw new NotImplementedException();
        }
    }
}
