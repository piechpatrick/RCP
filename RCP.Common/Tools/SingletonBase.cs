using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Tools
{
    public abstract class SingletonBase<TInstance, TFactory>
        where TInstance : SingletonBase<TInstance, TFactory>
        where TFactory : IInstanceFactory<TInstance>, new()
    {
        private static volatile TInstance s_instance;
        protected static readonly object s_syncRoot = new object();

        protected SingletonBase()
        {
        }

        public static TInstance Instance
        {
            get
            {
                if (s_instance == null)
                {
                    lock (s_syncRoot)
                    {
                        if (s_instance == null)
                        {
                            IInstanceFactory<TInstance> factory = new TFactory();
                            TInstance instance = factory.CreateInstance(s_syncRoot, false);

                            if (instance == null)
                            {
                                instance = factory.CreateInstance(s_syncRoot, true);
                                instance.OnInstanceCreated(true);
                            }
                            else
                                instance.OnInstanceCreated(false);

                            s_instance = instance;
                        }
                    }
                }

                return s_instance;
            }
        }

        protected abstract void OnInstanceCreated(bool isDefault);
    }
}
