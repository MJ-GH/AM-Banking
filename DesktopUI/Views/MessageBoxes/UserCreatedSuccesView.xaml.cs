using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
