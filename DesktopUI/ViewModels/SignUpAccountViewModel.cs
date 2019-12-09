using Caliburn.Micro;
using DesktopUI.Events;
using DesktopUI.Models;
using DesktopUI.ViewModels.MessageBoxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopUI.ViewModels
{
    public class SignUpAccountViewModel : Screen
    {
        private IEventAggregator _events;
        private IWindowManager manager = new WindowManager();
        private int _iD;
        private string _firstName;
        private string _lastName;
        private string _accountName;
        public static UserModel newUser;
        private BindingList<TypesModel> _someTypes = new BindingList<TypesModel>();
        private string _selectedUsertype;
        private string _selectedAccountType;
        private int _userTypeID;
        private int _accountTypeID;
        private bool canExecute = false;

        public int ID
        {
            get { return _iD; }
            set
            {
                _iD = value;
                NotifyOfPropertyChange(() => ID);
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
            }
        }
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                _accountName = value;
                NotifyOfPropertyChange(() => AccountName);
                NotifyOfPropertyChange(() => CanCreateUserAndAccount);
            }
        }
        public BindingList<TypesModel> SomeTypes
        {
            get { return _someTypes; }
            set
            {
                _someTypes = value;
                NotifyOfPropertyChange(() => SomeTypes);
            }
        }
        public string SelectedUserType
        {
            get { return _selectedUsertype; }
            set
            {
                _selectedUsertype = value;
                NotifyOfPropertyChange(() => SelectedUserType);
                NotifyOfPropertyChange(() => CanCreateUserAndAccount);
            }
        }
        public string SelectedAccountType
        {
            get { return _selectedAccountType; }
            set
            {
                _selectedAccountType = value;
                NotifyOfPropertyChange(() => SelectedAccountType);
                NotifyOfPropertyChange(() => CanCreateUserAndAccount);
            }
        }
        public int UserTypeID
        {
            get { return _userTypeID; }
            set
            {
                _userTypeID = value;
                NotifyOfPropertyChange(() => UserTypeID);
            }
        }
        public int AccountTypeID
        {
            get { return _accountTypeID; }
            set
            {
                _accountTypeID = value;
                NotifyOfPropertyChange(() => AccountTypeID);
            }
        }

        public SignUpAccountViewModel(UserModel _newUser, IEventAggregator events)
        {
            _events = events;
            newUser = _newUser;
            GetTypes();
        }


        public void ShowSignUpPage()
        {
            _events.PublishOnUIThread(new SignUpPageRequestEvent());
        }

        public void ShowLoginPage()
        {
            _events.PublishOnUIThread(new LogInPageRequestEvent());
        }

        public void GetTypes()
        {
            TypesModel type1 = new TypesModel();
            TypesModel type2 = new TypesModel();
            TypesModel type3 = new TypesModel();
            var addNew1 = true;
            var addNew2 = true;
            var addNew3 = true;

            using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
            {
                using (SqlCommand cmd = new SqlCommand("spHentTyper", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[0].ToString() == "Privat" && addNew1 == true)
                        {
                            type1.UserTypes = dr[0].ToString();
                            type1.AccountTypes.Add(dr[1].ToString());

                            addNew1 = false;
                        }
                        else if (dr[0].ToString() == "Privat" && addNew1 == false)
                        {
                            type1.AccountTypes.Add(dr[1].ToString());
                        }
                        else if (dr[0].ToString() == "Erhverv" && addNew2 == true)
                        {
                            type2.UserTypes = dr[0].ToString();
                            type2.AccountTypes.Add(dr[1].ToString());

                            addNew2 = false;
                        }
                        else if (dr[0].ToString() == "Erhverv" && addNew2 == false)
                        {
                            type2.AccountTypes.Add(dr[1].ToString());
                        }
                        else if (dr[0].ToString() == "Private Banking" && addNew3 == true)
                        {
                            type3.UserTypes = dr[0].ToString();
                            type3.AccountTypes.Add(dr[1].ToString());

                            addNew3 = false;
                        }
                        else if (dr[0].ToString() == "Private Banking" && addNew3 == false)
                        {
                            type3.AccountTypes.Add(dr[1].ToString());
                        }
                    }
                    SomeTypes.Add(type1);
                    SomeTypes.Add(type2);
                    SomeTypes.Add(type3);
                }
            }
        }
        
        public bool CanCreateUserAndAccount
        {
            get
            {
                bool output = false;

                if (AccountName?.Length > 0 && 
                    SelectedUserType?.Length > 0 && 
                    SelectedAccountType?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public void CreateUserAndAccount()
        {
            try
            {
                // Henter KundeTypeID
                try
                {
                    using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("spHentKundeTypeID", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@kundeType", SelectedUserType);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();

                                UserTypeID = reader.GetInt32(0);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("1) " + ex.Message);
                }

                // Opretter Bruger
                try
                {
                    using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("spOpretKunde", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@kundeTypeID", UserTypeID);
                        cmd.Parameters.AddWithValue("@fornavn", SignUpUserViewModel.newUser.FirstName);
                        cmd.Parameters.AddWithValue("@efternavn", SignUpUserViewModel.newUser.LastName);
                        cmd.Parameters.AddWithValue("@tlfnr", SignUpUserViewModel.newUser.PhoneNmb);
                        cmd.Parameters.AddWithValue("@email", SignUpUserViewModel.newUser.Email);
                        cmd.Parameters.AddWithValue("@kodeord", SignUpUserViewModel.newUser.Psw);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("2) " + ex.Message);
                }

                // Henter den nye brugers ID
                try
                {
                    using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
                    {
                        conn.Open();

                        using (SqlCommand cmd2 = new SqlCommand("spHentBrugerID", conn))
                        {
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@email", SignUpUserViewModel.newUser.Email);
                            cmd2.Parameters.AddWithValue("@kodeord", SignUpUserViewModel.newUser.Psw);

                            using (SqlDataReader reader = cmd2.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    ID = reader.GetInt32(0);
                                }
                            }
                        };
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("3) " + ex.Message);
                }

                // Henter KontoTypeID
                try
                {
                    using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand("spHentKontoTypeID", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@kontoType", SelectedAccountType);

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable ds = new DataTable();

                            adapter.Fill(ds);

                            if (ds.Rows.Count == 1)
                            {
                                AccountTypeID = ds.Rows[0].Field<int>(0);
                            }
                            else
                            {
                                if (SelectedUserType == "Privat")
                                {
                                    AccountTypeID = ds.Rows[0].Field<int>(0);
                                }
                                else if (SelectedUserType == "Erhverv")
                                {
                                    AccountTypeID = ds.Rows[1].Field<int>(0);
                                }
                                else if (SelectedUserType == "Private Banking")
                                {
                                    AccountTypeID = ds.Rows[2].Field<int>(0);
                                }
                            }
                        };
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("4) " + ex.Message);
                }

                // Opretter konto
                try
                {
                    using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("spOpretKonto", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@kundeID", ID);
                        cmd.Parameters.AddWithValue("@kontoTypeID", AccountTypeID);
                        cmd.Parameters.AddWithValue("@navn", AccountName);
                        cmd.ExecuteNonQuery();
                    }

                    canExecute = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("5) " + ex.Message);
                    canExecute = false;
                }
            }
            catch (Exception)
            {
                canExecute = false;
            }

            if (canExecute == true)
            {
                manager.ShowDialog(new UserCreatedSuccesViewModel());
                ShowLoginPage();
            }
        }
    }
}