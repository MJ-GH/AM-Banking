using Caliburn.Micro;
using DesktopUI.Events;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, 
        IHandle<SignUpPageRequestEvent>, 
        IHandle<LogInPageRequestEvent>, 
        IHandle<DashboardRequestEvent>
    {
        private DispatcherTimer dt = new DispatcherTimer();
        private SplashScreenViewModel _splashVM;
        private IEventAggregator _events;
        IWindowManager manager = new WindowManager();
        public static UserModel u;

        public double Width { get; private set; }
        public double Height { get; private set; }
        public double Left { get; private set; }
        public double Top { get; private set; }

        public ShellViewModel(SplashScreenViewModel splashVM, IEventAggregator events)
        {
            _events = events;
            _splashVM = splashVM;
            _events.Subscribe(this);
            
            ActivateItem(_splashVM);

            dt.Tick += new EventHandler(Dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 2);
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            dt.Stop();

            // Laver en ny instans af LoginViewModel
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(SignUpPageRequestEvent message)
        {
            // Laver en ny instans af SignUpViewModel hver gang, så brugerens data ikke tilfældigt bliver gemt.
            ActivateItem(IoC.Get<SignUpViewModel>());
        }

        public void Handle(LogInPageRequestEvent message)
        {
            // Laver en ny instans af LoginViewModel hver gang, så brugerens data ikke tilfældigt bliver gemt.
            ActivateItem(IoC.Get<LoginViewModel>());
        }


        public void Handle(DashboardRequestEvent message)
        {
            ActivateItem(IoC.Get<TestViewModel>());
            Application.Current.MainWindow.Height = 600;
            Application.Current.MainWindow.Width = 800;















            //manager.ShowWindow(new DashboardViewModel());

            //(GetView() as Window).Close();

        }

       

        


    }
}
