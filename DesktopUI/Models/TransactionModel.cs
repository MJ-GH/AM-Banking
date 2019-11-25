using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.Models
{
    public class TransactionModel
    {
        public int AccountNmb { get; set; }
        public string Note { get; set; }
        public char Function { get; set; }
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
        public DateTime Date { get; set; }
    }
}
