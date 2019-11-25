using Caliburn.Micro;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels.DashboardPages
{
    public class TransactionsViewModel : Screen
    {
        private int _accountNmb;
        private string _note;
        private char _function;
        private decimal _amount;
        private decimal _newBalance;
        private DateTime _date;
        private BindableCollection<TransactionModel> _transactions = new BindableCollection<TransactionModel>();

        public int AccountNmb
        {
            get { return _accountNmb; }
            set 
            { 
                _accountNmb = value;
                NotifyOfPropertyChange(() => AccountNmb);
            }
        }
        public string Note
        {
            get { return _note; }
            set 
            { 
                _note = value;
                NotifyOfPropertyChange(() => Note);
            }
        }
        public char Function
        {
            get { return _function; }
            set 
            { 
                _function = value;
                NotifyOfPropertyChange(() => Function);
            }
        }
        public decimal Amount
        {
            get { return _amount; }
            set 
            { 
                _amount = value;
                NotifyOfPropertyChange(() => Amount);
            }
        }
        public decimal NewBalance
        {
            get { return _newBalance; }
            set 
            { 
                _newBalance = value;
                NotifyOfPropertyChange(() => NewBalance);
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set 
            { 
                _date = value;
                NotifyOfPropertyChange(() => Date);
            }
        }
        public BindableCollection<TransactionModel> Transactions
        {
            get { return _transactions; }
            set 
            { 
                _transactions = value;
                NotifyOfPropertyChange(() => Transactions);
            }
        }

        public TransactionsViewModel()
        {
            GetTransactions();
        }

        public void GetTransactions()
        {
            using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
            {
                using (SqlCommand cmd = new SqlCommand("spHentAlleTransaktionerAlleKonti", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@kundeID", DashboardViewModel.u.Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Transactions.Add(new TransactionModel
                        {
                            AccountNmb = Convert.ToInt32(dr[0]),
                            Note = dr[1].ToString(),
                            Function = Convert.ToChar(dr[2]),
                            Amount = Convert.ToDecimal(dr[3]),
                            NewBalance = Convert.ToDecimal(dr[4]),
                            Date = Convert.ToDateTime(dr[5])
                        });
                    }
                }
            }
        }
    }
}
