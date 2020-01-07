using System;
using System.Windows;

namespace DesktopUI.Views.MessageBoxes
{
    /// <summary>
    /// Interaction logic for UserCreatedSuccesView.xaml
    /// </summary>
    public partial class UserCreatedSuccesView : Window
    {
        public UserCreatedSuccesView()
        {
            InitializeComponent();

            string current = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            TimeCreated.Content = current;
        }
    }
}