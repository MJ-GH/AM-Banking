using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpUserView : UserControl
    {
        public SignUpUserView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(FirstName);
        }
    }
}
