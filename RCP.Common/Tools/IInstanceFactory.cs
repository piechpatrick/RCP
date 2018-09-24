using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Tools
{
    public interface IInstanceFactory<T>
    {
        T CreateInstance(object syncRoot, bool forceDefault);
    }
}
