using System;
using System.Collections.Generic;
using RCP.Common.Tools;
namespace RCP.Common.Repositories
{
    public class XmlFileRepository<TContainer, TItem> : SynchronizedRepository<TItem>
        where TContainer : IRepository<TItem>, new()
    {
        public XmlFileRepository(string path)
        {
            this.Path = path;
        }
        public string Path { get; private set; }

        protected override IEnumerable<TItem> GetAllCore()
        {
            TContainer container = Loader.Load<TContainer>(this.Path, this.SyncRoot);

            if (container == null)
            {
                container = new TContainer();
            }

            return container.GetAll();
        }

        protected override bool TrySetCore(IEnumerable<TItem> items)
        {
            TContainer container = new TContainer();
            container.TrySet(items);
            return Loader.Save<TContainer>(this.Path, container, this.SyncRoot, true);
        }
    }
}
