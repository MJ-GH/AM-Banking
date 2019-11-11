using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels
{
    public class SignUpViewModel : Screen
    {
        private readonly IEventAggregator _eventAggragator;

        public SignUpViewModel(IEventAggregator eventAggregator)
        {
            _eventAggragator = eventAggregator;
        }

        public void ShowLogin()
        {
            _eventAggragator.PublishOnUIThread(new Helpers.ViewChangeHelper { Page = new LoginViewModel() });
        }

        public SignUpViewModel()
        {

        }




        //private string _firstName;
        //private string _lastName;
        //private BindableCollection<UserModel> _accounts = new BindableCollection<UserModel>();

        // Properties
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

        //public BindableCollection<UserModel> Accounts
        //{
        //    get { return _accounts; }
        //    set { _accounts = value; }
        //}

    }
}
