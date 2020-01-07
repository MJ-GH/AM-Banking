namespace DesktopUI.Models
{
    public class TransactionModel
    {
        public int TransactionID { get; set; }
        public int AccountNmb { get; set; }
        public string Note { get; set; }
        public char Function { get; set; }
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
        public string Date { get; set; }
    }
}