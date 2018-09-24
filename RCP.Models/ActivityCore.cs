using RCP.Common;
using RCP.Common.Interfaces;
using System;
using System.Xml.Serialization;

namespace RCP.Models
{
    public class ActivityCore : IActivity
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [XmlElement(typeof(XmlTimeSpan))]
        public TimeSpan TimeSpan { get; set; }

        public bool Finalised { get; set; }

        public ActivityCore()
        {

        }
        public ActivityCore(IActivity activity)
        {
            this.Name = activity.Name;
            this.StartDate = activity.StartDate;
            this.EndDate = activity.EndDate;
            this.TimeSpan = activity.TimeSpan;
            this.Finalised = activity.Finalised;
        }
    }
}
