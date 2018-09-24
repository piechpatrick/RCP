using RCP.Common.Factories;
using RCP.Common.Repositories;
using RCP.Core.Repositories;
using RCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Core.Factories
{
    internal class RepositoriesCreator
    {
        public RepositoriesCreator()
        {
            this.Activities = new ActivityRepository(AppDomain.CurrentDomain.BaseDirectory + "Config");      
            this.ActivitiesRepositoryFactory = new SavableRepositoryFactory<ActivityCore>(this.Activities);

        }


        private ISavableRepository<ActivityCore> Activities { get; set; }

        public ISavableRepositoryFactory<ActivityCore> ActivitiesRepositoryFactory { get; private set; }
    }
}
