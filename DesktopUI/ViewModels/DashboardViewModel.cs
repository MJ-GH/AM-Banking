using Caliburn.Micro;
using DesktopUI.Models;
using DesktopUI.ViewModels.DashboardPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels
{
    public class DashboardViewModel : Conductor<object>
    {
        public static UserModel u;
        public DashboardViewModel(UserModel _u)
        {
             u = _u;

            ActivateItem(IoC.Get<MainMenuViewModel>());
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
    }
}
