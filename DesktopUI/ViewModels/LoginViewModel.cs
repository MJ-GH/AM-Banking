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

        public LoginViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public void ShowSignUp()
        {
            _events.PublishOnUIThread(new SignUpPageRequestEvent());
        }


        //private BindableCollection<UserModel> _accounts = new BindableCollection<UserModel>();
        //public string FirstName
        //{
        //    get { return _firstName; }
        //    set
        //    {
        //        _firstName = value;
        //        NotifyOfPropertyChange(() => FirstName);
        //        NotifyOfPropertyChange(() => FullName);
        //    }
        //}

        //public string LastName
        //{
        //    get { return _lastName; }
        //    set
        //    {
        //        _lastName = value;
        //        NotifyOfPropertyChange(() => LastName);
        //        NotifyOfPropertyChange(() => FullName);
        //    }
        //}

        //public string FullName
        //{
        //    get { return $"{ FirstName } { LastName }"; }
        //}


    }
}
