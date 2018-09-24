using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RCP.Common.Repositories
{
    public class MemoryRepository<TItem> : ISavableRepository<TItem>
    {
        private List<TItem> items;
        private ISavableRepository<TItem> source;

        public MemoryRepository(List<TItem> items)
        {
            if (items == null)
            {
                this.items = new List<TItem>();
                this.source = new MemoryRepository<TItem>(this.items);
            }
            else
            {
                this.items = items;
                this.source = this;
            }
        }

        public MemoryRepository(ISavableRepository<TItem> source)
        {
            this.source = source;
        }

        private List<TItem> Items
        {
            get
            {
                if (this.items == null || this.items.Count == 0)
                {
                    var list = new List<TItem>();
                    List<TItem> original = Interlocked.CompareExchange(ref this.items, list, null);
                    if (original == null)
                    {
                        var itemsResult = this.source.GetAll();
                        return this.items = itemsResult.ToList();
                    }
                    else
                    {
                        return original;
                    }
                }
                else
                {
                    return this.items;
                }
            }
        }


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

        public Task<IEnumerable<TItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool TryAdd(TItem item)
        {
            this.Items.Add(item);
            return true;
        }

        public bool TryAdd(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                this.Items.Add(item);
            return true;
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
            throw new NotImplementedException();
        }

        public bool TrySet(IEnumerable<TItem> items)
        {
            return this.source.TrySet(items);
        }

        public Task<bool> TrySetAsync(IEnumerable<TItem> items)
        {
            throw new NotImplementedException();
        }
    }
}
