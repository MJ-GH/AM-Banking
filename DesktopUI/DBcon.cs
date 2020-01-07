namespace DesktopUI
{
    class DBcon
    {
        public static string CS = "Data Source = 10.0.6.1\\AEM_BANKING; Initial Catalog = AM_Banking_db; User Id = sa; Password = Passw0rd!";

        public static string Connect()
        {
            return CS;
        }
    }
}