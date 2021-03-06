﻿using Caliburn.Micro;
using DesktopUI.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DesktopUI.ViewModels.DashboardPages
{
    public class AccountsViewModel : Screen
    {
        private string _accountNmb;
        private string _accountType;
        private string _accountName;
        private decimal _balance;
        private BindableCollection<AccountModel> _accounts = new BindableCollection<AccountModel>();

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

        public AccountsViewModel()
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
                        Accounts.Add(new AccountModel(
                            AccountNmb = dr[0].ToString(),
                            AccountType = dr[1].ToString(),
                            AccountName = dr[2].ToString(),
                            Balance = Convert.ToDecimal(dr[3])));
                    }
                }
            }
        }
    }
}