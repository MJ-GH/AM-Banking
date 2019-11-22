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
        public string Name { get; set; }
        public decimal Balance { get; set; }

        //public AccountModel(int accountNmb, string name, decimal balance)
        //{
        //    AccountNmb = accountNmb;
        //    Name = name;
        //    Balance = balance;
        //}
    }
}
