using Caliburn.Micro;
using DesktopUI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private IEventAggregator _events;

        public LoginViewModel() { }
        public LoginViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public void ShowSignUp()
        {
            _events.PublishOnUIThread(new SignUpPageRequestEvent());
        }

        

    }
}
