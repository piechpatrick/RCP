using RCP.Common.Repositories;
using RCP.Models;
using System.IO;

namespace RCP.Core.Repositories
{
    internal class ActivityRepository : SavableRepositoryBase<ActivityCore>
    {
        public ActivityRepository(string configPath)
            : base()
        {
            /*
             Tutaj jako source tworzymy konkretne repo
            */
            string path = Path.Combine(configPath, "Activities.xml");

            ISavableRepository<ActivityCore> xmlrepo = new XmlFileRepository<ActivityContainer, ActivityCore>(path);
            this.Source = new MemoryRepository<ActivityCore>(xmlrepo);
        }

        public ActivityRepository(IRepository<ActivityCore> source)
            : base(source)
        {

        }
    }
}
