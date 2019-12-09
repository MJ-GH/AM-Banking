using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.Models
{
    public class AccountModel
    {
        public int AccountNmb { get; set; }
        public int AccountTypeID { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }

        public AccountModel(int accountTypeID)
        {
            AccountTypeID = accountTypeID;
        }

        public AccountModel(int accountNmb, string accountType, string accountName, decimal balance)
        {
            AccountNmb = accountNmb;
            AccountType = accountType;
            AccountName = accountName;
            Balance = balance;
        }
    }
}
