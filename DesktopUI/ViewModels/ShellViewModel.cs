﻿using Caliburn.Micro;
using DesktopUI.Events;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, 
        IHandle<SignUpPageRequestEvent>, 
        IHandle<LogInPageRequestEvent>, 
        IHandle<DashboardRequestEvent>,
        IHandle<SignUpAccountPageRequest>
    {
        private DispatcherTimer dt = new DispatcherTimer();
        private IEventAggregator _events;
        private IWindowManager _manager = new WindowManager();
        public static UserModel u;

        public ShellViewModel(IEventAggregator events, UserModel _u)
        {
            _events = events;
            _events.Subscribe(this);
            u = _u;

            ActivateItem(IoC.Get<SplashScreenViewModel>());

            dt.Tick += new EventHandler(Dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 2);
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            dt.Stop();

            // Laver en ny instans af LoginViewModel
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(SignUpPageRequestEvent message)
        {
            // Laver en ny instans af SignUpViewModel hver gang, så brugerens data ikke tilfældigt bliver gemt.
            ActivateItem(IoC.Get<SignUpUserViewModel>());
        }
        public void Handle(SignUpAccountPageRequest message)
        {
            ActivateItem(IoC.Get<SignUpAccountViewModel>());
        }
        public void Handle(LogInPageRequestEvent message)
        {
            // Laver en ny instans af LoginViewModel hver gang, så brugerens data ikke tilfældigt bliver gemt.
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(DashboardRequestEvent message)
        {
            _manager.ShowWindow(new DashboardViewModel(u, _events));

            TryClose();
        }

        public string Copyright
        {
            get { return $"{ DateTime.Now.Year }"; }
        }
    }
}
