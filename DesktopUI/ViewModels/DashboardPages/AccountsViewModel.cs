using Caliburn.Micro;
using DesktopUI.ViewModels.DashboardPages.AccountsPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels.DashboardPages
{
    public class AccountsViewModel : Conductor<object>
    {
        public AccountsViewModel()
        {
            ActivateItem(IoC.Get<AccountsOverviewViewModel>());
        }

    }
}
