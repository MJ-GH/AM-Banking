using Caliburn.Micro;
using DesktopUI.Events;
using DesktopUI.Models;
using DesktopUI.ViewModels.MessageBoxes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Media;

namespace DesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private IEventAggregator _events;
        IWindowManager manager = new WindowManager();
        private string _email;
        private string _psw;
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

        public LoginViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public void ShowSignUp()
        {
            _events.PublishOnUIThread(new SignUpUserPageRequest());
        }

        public void Login()
        {
            using (SqlConnection conn = new SqlConnection(DBcon.Connect()))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("spLogin", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@email", Email);
                    cmd.Parameters.AddWithValue("@kodeord", Psw);
                    cmd.ExecuteNonQuery();

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        using (SqlCommand cmd2 = new SqlCommand("spHentbrugerinfo", conn))
                        {
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@email", Email);
                            cmd2.Parameters.AddWithValue("@kodeord", Psw);

                            using (SqlDataReader reader = cmd2.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();

                                    ShellViewModel.u = new UserModel(
                                        reader.GetInt32(0), 
                                        reader.GetString(1).ToString(), 
                                        reader.GetString(2).ToString());
                                }
                            }
                        };

                        _events.PublishOnUIThread(new DashboardRequest());
                    }
                    else
                    {
                        manager.ShowDialog(new LoginErrorViewModel());
                        Psw = "";
                    }
                }
                catch (Exception)
                {
                    manager.ShowDialog(new LoginErrorViewModel());
                    Psw = "";
                }
            }
        }
    }
}
