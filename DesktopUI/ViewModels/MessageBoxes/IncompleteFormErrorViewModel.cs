using Caliburn.Micro;

namespace DesktopUI.ViewModels.MessageBoxes
{
    public class IncompleteFormErrorViewModel :  Screen
    {
        public void CloseWindow()
        {
            TryClose();
        }
    }
}
