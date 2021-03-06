﻿namespace DesktopUI.Models
{
    public class AccountModel
    {
        public string AccountNmb { get; set; }
        public int AccountTypeID { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }

        public AccountModel(int accountTypeID)
        {
            AccountTypeID = accountTypeID;
        }

        public AccountModel(string accountName, string accountNmb, decimal balance)
        {
            AccountName = accountName;
            AccountNmb = accountNmb;
            Balance = balance;
        }

        public AccountModel(string accountNmb, string accountType, string accountName, decimal balance)
        {
            AccountNmb = accountNmb;
            AccountType = accountType;
            AccountName = accountName;
            Balance = balance;
        }
    }
}