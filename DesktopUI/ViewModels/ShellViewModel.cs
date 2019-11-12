using Caliburn.Micro;
using DesktopUI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<SignUpPageRequestEvent>, IHandle<LogInPageRequestEvent>
    {
        private DispatcherTimer dt = new DispatcherTimer();
        private SplashScreenViewModel _splashVM;
        private IEventAggregator _events;
        private SimpleContainer _container;

        public ShellViewModel(SplashScreenViewModel splashVM, IEventAggregator events, SimpleContainer container)
        {
            _events = events;
            _splashVM = splashVM;
            _container = container;

            _events.Subscribe(this);
            
            ActivateItem(_splashVM);

            dt.Tick += new EventHandler(Dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 2);
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            dt.Stop();

            // Laver en ny instans af LoginViewModel
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(SignUpPageRequestEvent message)
        {
            // Laver en ny instans af SignUpViewModel hver gang, så brugerens data ikke tilfældigt bliver gemt.
            ActivateItem(_container.GetInstance<SignUpViewModel>());
        }

        public void Handle(LogInPageRequestEvent message)
        {
            // Laver en ny instans af LoginViewModel hver gang, så brugerens data ikke tilfældigt bliver gemt.
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        
    }
}
