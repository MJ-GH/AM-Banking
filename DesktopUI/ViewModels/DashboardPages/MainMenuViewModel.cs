using Caliburn.Micro;
using DesktopUI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopUI.ViewModels.DashboardPages
{
    public class MainMenuViewModel : Screen
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => FullName);
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => FullName);
            }
        }
        public string FullName
        {
            get { return $"{ FirstName } { LastName }"; }
        }

        IWindowManager manager = new WindowManager();
        private IEventAggregator _events;

        public MainMenuViewModel(IEventAggregator events)
        {
            _firstName = DashboardViewModel.u.FirstName;
            _lastName = DashboardViewModel.u.LastName;

            _events = events;
        }

        public void LogOut()
        {
            _events.PublishOnUIThread(new LogOutEvent());
        }





    }
}
