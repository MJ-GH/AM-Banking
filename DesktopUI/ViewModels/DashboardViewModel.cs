using Caliburn.Micro;
using DesktopUI.Events;
using DesktopUI.Models;
using DesktopUI.ViewModels.DashboardPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopUI.ViewModels
{
    public class DashboardViewModel : Conductor<object>,
        IHandle<LogOutEvent>
    {
        public static UserModel u;
        IEventAggregator _events;
        IWindowManager _manager = new WindowManager();

        public DashboardViewModel(UserModel _u, IEventAggregator events)
        {
             u = _u;
            _events = events;
            _events.Subscribe(this);

            ShowMainMenuPage();
        }

        public void ShowMainMenuPage()
        {
            ActivateItem(IoC.Get<MainMenuViewModel>());
        }

        public void ShowAccountsPage()
        {
            ActivateItem(IoC.Get<AccountsViewModel>());
        }

        public void ShowTransactionsPage()
        {
            ActivateItem(IoC.Get<TransactionsViewModel>());
        }

        public void ShowPaymentsPage()
        {
            ActivateItem(IoC.Get<PaymentsViewModel>());
        }

        public void Handle(LogOutEvent message)
        {
            u = null;
            _manager.ShowWindow(new ShellViewModel(_events, u));

            //DashboardViewModel dash = this;
            //dash.TryClose();
            TryClose();

            //Application.Current.MainWindow.Close();
            //(GetView() as Window).Close();
        }
    }
}
