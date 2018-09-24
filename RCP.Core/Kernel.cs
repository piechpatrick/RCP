using RCP.Common.Repositories;
using RCP.Common.Tools;
using RCP.Core.Factories;
using RCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Core
{
    public class Kernel : SingletonBase<Kernel,Kernel.KernelFactory>
    {

        public ISavableRepository<ActivityCore> ActivityRepository { get; private set; }

        public Kernel()
        {
            var creator = new RepositoriesCreator();
            this.ActivityRepository = creator.ActivitiesRepositoryFactory.CreateRepository();
            this.ActivityRepository.GetAll();
        }


        protected override void OnInstanceCreated(bool isDefault)
        {
            
        }

        public sealed class KernelFactory : IInstanceFactory<Kernel>
        {
            public KernelFactory()
            {
            }

            public Kernel CreateInstance(object syncRoot, bool forceDefault)
            {
                return new Kernel();
            }
        }
    }
}
