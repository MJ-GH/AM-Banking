using Caliburn.Micro;

namespace DesktopUI.ViewModels.MessageBoxes
{
    public class LoginErrorViewModel : Screen
    {
        public void CloseWindow()
        {
            TryClose();
        }
    }
}
