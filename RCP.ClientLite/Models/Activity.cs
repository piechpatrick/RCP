using Prism.Commands;
using Prism.Mvvm;
using RCP.Common.Enums;
using RCP.Common.Interfaces;
using RCP.Common.Tools;
using RCP.Common.Tools.RCP.Common.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RCP.ClientLite.Models
{
    public class Activity : BindableBase, IActivity
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        Stopwatch sw = new Stopwatch();

        private string name;

        private DateTime startDate;

        private DateTime endDate;

        private TimeSpan timeSpan;

        private bool finalised;

        private bool isSmart;

        private ActivityState activityState;

        private ICommand startCommand;

        private ICommand stopCommand;

        private ICommand pauseCommand;

        private GlobalKeyboardHook keyboardHook;

        private int afkTicks;



        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }


        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.SetProperty(ref this.startDate, value); }
        }

        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.SetProperty(ref this.endDate, value); }
        }

        public TimeSpan TimeSpan
        {
            get { return this.timeSpan; }
            set { this.SetProperty(ref this.timeSpan, value); }
        }

        public bool Finalised
        {
            get { return this.finalised; }
            set { this.SetProperty(ref this.finalised, value); }
        }

        public bool IsSmart
        {
            get { return this.isSmart; }
            set
            {
                this.SetProperty(ref this.isSmart, value);
                this.PauseOrResume();
            }
        }

        public ActivityState ActivityState
        {
            get { return this.activityState; }
            set { this.SetProperty(ref this.activityState, value); }
        }

        public int AfkTicks
        {
            get { return this.afkTicks; }
            set { this.SetProperty(ref this.afkTicks, value); }
        }

        public Activity()
        {
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            this.keyboardHook = new GlobalKeyboardHook();
            this.keyboardHook.KeyboardPressed += KeyboardHook_KeyboardPressed;
            MouseHook.MouseAction += MouseHook_MouseAction;
            this.ActivityState = ActivityState.None;
            this.AfkTicks = 30;
        }


        public Activity(IActivity activity)
            :this()
        {
            this.Name = activity.Name;
            this.StartDate = activity.StartDate;
            this.EndDate = activity.EndDate;
            this.TimeSpan = activity.TimeSpan;
            this.Finalised = activity.Finalised;
        }

        public ICommand StartCommand => this.startCommand == null
            ? startCommand = new DelegateCommand(this.Start) : this.startCommand;
        public ICommand StopCommand => this.stopCommand == null
            ? stopCommand = new DelegateCommand(this.Stop) : this.stopCommand;
        public ICommand PauseCommand => this.pauseCommand == null
            ? pauseCommand = new DelegateCommand(this.Pause) : this.pauseCommand;

        public void Start()
        {
            this.dispatcherTimer.Start();
            this.sw.Start();
            this.StartDate = DateTime.Now;
            this.ActivityState = ActivityState.Started;
        }

        public void Stop()
        {
            this.dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this.sw = new Stopwatch();
            this.TimeSpan = sw.Elapsed;
            this.EndDate = DateTime.Now;
            this.ActivityState = ActivityState.Stopped;
        }

        public void Pause()
        {
            this.sw.Stop();
            this.dispatcherTimer.Stop();
            this.ActivityState = ActivityState.Paused;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)            
                this.TimeSpan = sw.Elapsed;          
        }

        private void KeyboardHook_KeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            this.afkTemp = 0;
            this.reStartTimers();
        }

        private void MouseHook_MouseAction(object sender, EventArgs e)
        {
            this.afkTemp = 0;
            this.reStartTimers();
        }

        private void reStartTimers()
        {
            if (this.IsSmart && (this.ActivityState == ActivityState.Paused
                || this.ActivityState == ActivityState.Stopped))
            {
                this.dispatcherTimer.Start();
                this.sw.Start();
            }
        }

        private int afkTemp = 0;
        private async void PauseOrResume()
        {
            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    afkTemp++;
                    if (afkTemp > this.AfkTicks && this.IsSmart)
                        this.Pause();
                    await Task.Delay(1000);
                }
            });
        }
      
    }
}
