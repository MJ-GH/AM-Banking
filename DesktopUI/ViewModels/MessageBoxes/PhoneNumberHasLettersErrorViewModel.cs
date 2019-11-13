using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels.MessageBoxes
{
    public class PhoneNumberHasLettersErrorViewModel : Screen
    {
        public void CloseWindow()
        {
            TryClose();
        }
    }
}
