using Caliburn.Micro;
using DesktopUI.Events;
using DesktopUI.Models;
using DesktopUI.ViewModels.MessageBoxes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DesktopUI.ViewModels
{
    public class SignUpUserViewModel : Screen
    {
        private IEventAggregator _events;
        IWindowManager manager = new WindowManager();
        private string _firstName;
        private string _lastName;
        private string _phoneNmb;
        private string _email;
        private string _psw;
        private string _pswDup;
        private bool isValidationOkay = false;
        public static UserModel newUser;

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
        public string PhoneNmb
        {
            get { return _phoneNmb; }
            set
            {
                _phoneNmb = value;
                NotifyOfPropertyChange(() => PhoneNmb);
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }
        public string Psw
        {
            get { return _psw; }
            set
            {
                _psw = value;
                NotifyOfPropertyChange(() => Psw);
            }
        }
        public string PswDup
        {
            get { return _pswDup; }
            set
            {
                _pswDup = value;
                NotifyOfPropertyChange(() => PswDup);
            }
        }

        public SignUpUserViewModel(IEventAggregator events)
        {
            _events = events;
        }


        public void ShowLoginPage()
        {
            _events.PublishOnUIThread(new LogInPageRequest());
        }

        public void ShowSignUpAccountPage()
        {
            _events.PublishOnUIThread(new SignUpAccountPageRequest());
        }

        public void ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(PhoneNmb) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Psw) ||
                string.IsNullOrWhiteSpace(PswDup))
            {
                manager.ShowDialog(new IncompleteFormErrorViewModel());
            }
            else if (PhoneNmb.Length != 8)
            {
                manager.ShowDialog(new PhoneNumberLengthErrorViewModel());
            }
            else if (!int.TryParse(PhoneNmb, out int parsedValue))
            {
                manager.ShowDialog(new PhoneNumberHasLettersErrorViewModel());
            }
            else if (Psw != PswDup)
            {
                manager.ShowDialog(new PasswordsDoesntMatchErrorViewModel());
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
                    {
                        conn.Open();

                        SqlCommand cmd1 = new SqlCommand("spTjekTlfnrDuplikation", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd1.Parameters.AddWithValue("@tlfnr", PhoneNmb);

                        int count = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (count == 1)
                        {
                            MessageBox.Show("Telefonnummeret er allerede i brug!", "AM Banking - Telefonnummer Fejl!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            isValidationOkay = false;
                        }
                        else
                        {
                            SqlCommand cmd2 = new SqlCommand("spTjekEmailDuplikation", conn)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmd2.Parameters.AddWithValue("@email", Email);

                            int count2 = Convert.ToInt32(cmd2.ExecuteScalar());
                            if (count2 == 1)
                            {
                                MessageBox.Show("Emailen er allerede i brug!", "AM Banking - Email Fejl!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                isValidationOkay = false;
                            }
                            else
                            {
                                isValidationOkay = true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    isValidationOkay = false;
                }

                if (isValidationOkay == true)
                {
                    newUser = new UserModel(FirstName, LastName, PhoneNmb, Email, Psw);
                    ShowSignUpAccountPage();
                }
            }
        }
    }
}