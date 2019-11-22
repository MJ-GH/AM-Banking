using Caliburn.Micro;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopUI.ViewModels.DashboardPages.AccountsPages
{
    public class AccountsOverviewViewModel : Screen
    {
        private int _accountNmb;
        private string _name;
        private decimal _balance;
        private BindableCollection<AccountModel> _accounts = new BindableCollection<AccountModel>();
        IWindowManager manager = new WindowManager();

        public int AccountNmb
        {
            get { return _accountNmb; }
            set
            {
                _accountNmb = value;
                NotifyOfPropertyChange(() => AccountNmb);
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        public decimal Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                NotifyOfPropertyChange(() => Balance);
            }
        }
        public BindableCollection<AccountModel> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                NotifyOfPropertyChange(() => Accounts);
            }
        }


        public AccountsOverviewViewModel()
        {
            GetAccounts();
        }

        public void GetAccounts()
        {
            using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
            {
                using (SqlCommand cmd = new SqlCommand("spHentKonti", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@kundeID", DashboardViewModel.u.Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Accounts.Add(new AccountModel
                        {
                            AccountNmb = Convert.ToInt32(dr[0]),
                            Name = dr[1].ToString(),
                            Balance = Convert.ToDecimal(dr[2])
                        });
                    }
                }
            }
        }
    }
}
