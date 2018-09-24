using RCP.ClientLite.Models;
using RCP.Common.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP.ClientLite
{
    public class Visualization : SingletonBase<Visualization,Visualization.VisualizationFactory>
    {

        private ObservableCollection<Activity> activities;



        public Visualization()
        {
            this.activities = new ObservableCollection<Activity>();
        }

        public ObservableCollection<Activity> Activities
        {
            get { return this.activities; }
        }

        public void AddActivity(Activity activity)
        {
            this.activities.Add(activity);
        }

        public void DeleteActivity(Activity activity)
        {
            this.activities.Remove(activity);      
        }




        protected override void OnInstanceCreated(bool isDefault)
        {
            
        }

        public sealed class VisualizationFactory : IInstanceFactory<Visualization>
        {
            public VisualizationFactory()
            {
            }

            public Visualization CreateInstance(object syncRoot, bool forceDefault)
            {
                return new Visualization();
            }
        }
    }
}
