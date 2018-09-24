using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.Common.Interfaces
{
    public interface IActivity
    {
        string Name { get; set; }

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        TimeSpan TimeSpan { get; set; }

        bool Finalised { get; set; }
    }
}
