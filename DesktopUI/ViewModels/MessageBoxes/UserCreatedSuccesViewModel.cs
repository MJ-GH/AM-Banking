using Caliburn.Micro;

namespace DesktopUI.ViewModels.MessageBoxes
{
    public class UserCreatedSuccesViewModel : Screen
    {
        public void CloseWindow()
        {
            TryClose();
        }
    }
}
