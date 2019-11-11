using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<Helpers.ViewChangeHelper>
    {
        DispatcherTimer dt = new DispatcherTimer();
        private readonly SplashScreenViewModel _splashVM;
        private readonly LoginViewModel _loginVM;
        private readonly SignUpViewModel _signUpVM;
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(SplashScreenViewModel splashVM, LoginViewModel loginVM, SignUpViewModel signUpVM, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _loginVM = loginVM;
            _signUpVM = signUpVM;
            _splashVM = splashVM;
            ActivateItem(_splashVM);

            dt.Tick += new EventHandler(Dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 2);
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            dt.Stop();
            ActivateItem(_loginVM);
        }

        public void Handle(Helpers.ViewChangeHelper message)
        {
            ActivateItem(message.Page);
        }





    }
}
