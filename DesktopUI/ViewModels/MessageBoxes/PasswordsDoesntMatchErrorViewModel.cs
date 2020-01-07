using Caliburn.Micro;

namespace DesktopUI.ViewModels.MessageBoxes
{
    public class PasswordsDoesntMatchErrorViewModel : Screen
    {
        public void CloseWindow()
        {
            TryClose();
        }
    }
}