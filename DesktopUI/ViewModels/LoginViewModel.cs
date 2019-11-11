using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void ShowSignUp()
        {
            _eventAggregator.PublishOnUIThread(new Helpers.ViewChangeHelper { Page = new SignUpViewModel() });
        }

        public LoginViewModel()
        {

        }


    }
}
