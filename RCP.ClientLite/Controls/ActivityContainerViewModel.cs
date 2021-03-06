﻿using Prism.Commands;
using Prism.Mvvm;
using RCP.ClientLite.Models;
using RCP.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RCP.ClientLite.Controls
{
    public class ActivityContainerViewModel : BindableBase
    {
        private ICommand addActivityCommand;
        private ICommand deleteActivitiesCommand;
        private ICommand saveActivityCommand;

        public ActivityContainerViewModel()
        {
            
        }

        public ICommand AddActivityCommand => this.addActivityCommand == null 
            ? addActivityCommand = new DelegateCommand(this.addActivity) : this.addActivityCommand;

        public ICommand DeleteActivityCommand => this.deleteActivitiesCommand == null 
            ? deleteActivitiesCommand = new DelegateCommand<object>(this.deleteActivities) : this.deleteActivitiesCommand;

        public ICommand SaveActivityCommand => this.saveActivityCommand == null
            ? saveActivityCommand = new DelegateCommand<object>(this.saveActivities) : this.saveActivityCommand;

        public ObservableCollection<Activity> Activities
        {
            get { return Visualization.Instance.Activities; }
        }
        

        private void addActivity()
        {
            var activity = new Activity();
            activity.Name = "ex. name, task id";
            Visualization.Instance.AddActivity(activity);
        }

        private void deleteActivities(object ob)
        {
            if(ob != null && (ob as IList).Count > 0)
            {
                var items = ob as IList;
                foreach (var act in items.Cast<Activity>().ToList())
                {
                    Visualization.Instance.DeleteActivity(act);
                }               
            }
        }

        private void saveActivities(object ob)
        {
            if (ob != null && (ob as IList).Count > 0)
            {
                var items = ob as IList;
                items.Cast<Activity>().ToList().ForEach(a => a.EndDate = DateTime.Now);
                var cores = items.Cast<Activity>().ToList().Select(a => new RCP.Models.ActivityCore(a));
                Kernel.Instance.ActivityRepository.TryAdd(cores);
                Kernel.Instance.ActivityRepository.TrySave();

                foreach (var act in items.Cast<Activity>().ToList())
                {
                    act.EndDate = DateTime.Now;
                    this.Activities.Remove(act);
                }
            }
        }

    }
}
