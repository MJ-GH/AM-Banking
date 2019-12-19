using Caliburn.Micro;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace DesktopUI.ViewModels.DashboardPages
{
    public class PaymentsViewModel : Screen
    {
        private string _accountNmb;
        private string _accountType;
        private string _accountName;
        private decimal _balance;
        private ObservableCollection<AccountModel> _sender = new ObservableCollection<AccountModel>();
        private string _selectedAccountNmb;
        private decimal _amount;
        private string _note;
        private string _receiver;

        public string AccountNmb
        {
            get { return _accountNmb; }
            set
            {
                _accountNmb = value;
                NotifyOfPropertyChange(() => AccountNmb);
            }
        }
        public string AccountType
        {
            get { return _accountType; }
            set
            {
                _accountType = value;
                NotifyOfPropertyChange(() => AccountType);
            }
        }
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                _accountName = value;
                NotifyOfPropertyChange(() => AccountName);
            }
        }
        public decimal Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                NotifyOfPropertyChange(() => Balance);
                NotifyOfPropertyChange(() => CanMakePayment);
            }
        }
        public ObservableCollection<AccountModel> Sender
        {
            get { return _sender; }
            set
            {
                _sender = value;
                NotifyOfPropertyChange(() => Sender);
                NotifyOfPropertyChange(() => CanMakePayment);
            }
        }
        public string SelectedAccountNmb
        {
            get { return _selectedAccountNmb; }
            set
            {
                _selectedAccountNmb = value;

                for (int i = 0; i < Sender.Count; i++)
                {
                    if (Sender[i].AccountNmb == SelectedAccountNmb)
                    {
                        Balance = Sender[i].Balance;
                    }
                }

                NotifyOfPropertyChange(() => SelectedAccountNmb);
                NotifyOfPropertyChange(() => Balance);
                NotifyOfPropertyChange(() => CanMakePayment);

            }
        }
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                NotifyOfPropertyChange(() => Amount);
                NotifyOfPropertyChange(() => CanMakePayment);
            }
        }
        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                NotifyOfPropertyChange(() => Note);
                NotifyOfPropertyChange(() => CanMakePayment);
            }
        }
        public string Receiver
        {
            get { return _receiver; }
            set
            {
                _receiver = value;
                NotifyOfPropertyChange(() => Receiver);
                NotifyOfPropertyChange(() => CanMakePayment);
            }
        }

        public PaymentsViewModel()
        {
            GetSenders();
        }


        public void GetSenders()
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
                        Sender.Add(new AccountModel(
                            AccountNmb = dr[0].ToString(),
                            AccountType = dr[1].ToString(),
                            AccountName = dr[2].ToString(),
                            Balance = Convert.ToDecimal(dr[3])));
                    }
                }
            }
        }

        public bool CanMakePayment
        {
            get
            {
                bool output = false;

                if (SelectedAccountNmb?.Length > 0 &&
                    Note?.Length > 0 &&
                    Receiver?.Length > 0 &&
                    SelectedAccountNmb != Receiver &&
                    Amount > 0 &&
                    Amount <= Balance)
                {
                    output = true;
                }

                return output;
            }
        }

        public void MakePayment()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("spLavBetaling", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@afsender", SelectedAccountNmb);
                        cmd.Parameters.AddWithValue("@modtager", Receiver);
                        cmd.Parameters.AddWithValue("@note", Note);
                        cmd.Parameters.AddWithValue("@beloeb", Amount);

                        int count = Convert.ToInt32(cmd.ExecuteNonQuery());

                        if (count == -1)
                        {
                            MessageBox.Show("Modtageren findes ikke!");
                        }
                        else
                        {
                            MessageBox.Show("Jeg tror det lykkedes?");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
