using Caliburn.Micro;
using DesktopUI.Events;
using DesktopUI.Models;
using System;
using System.Windows.Threading;

namespace DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, 
        IHandle<SignUpUserPageRequest>, 
        IHandle<LogInPageRequest>, 
        IHandle<DashboardRequest>,
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

            dt.Tick += new EventHandler(DtOver);
            dt.Interval = new TimeSpan(0, 0, 2);
            dt.Start();
        }

        private void DtOver(object sender, EventArgs e)
        {
            dt.Stop();

            // Laver en ny instans af LoginViewModel
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(SignUpUserPageRequest message)
        {
            // Laver en ny instans af SignUpViewModel hver gang, så brugerens data ikke tilfældigt bliver gemt.
            ActivateItem(IoC.Get<SignUpUserViewModel>());
        }
        public void Handle(SignUpAccountPageRequest message)
        {
            ActivateItem(IoC.Get<SignUpAccountViewModel>());
        }
        public void Handle(LogInPageRequest message)
        {
            // Laver en ny instans af LoginViewModel hver gang, så brugerens data ikke tilfældigt bliver gemt.
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(DashboardRequest message)
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