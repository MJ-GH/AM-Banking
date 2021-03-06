﻿using Caliburn.Micro;
using DesktopUI.Events;
using System;

namespace DesktopUI.ViewModels.DashboardPages
{
    public class MainMenuViewModel : Screen
    {
        private string msg;
        private string _firstName;
        private string _lastName;
        private IEventAggregator _events;
        private int _timeOfDay;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => FullName);
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => FullName);
            }
        }
        public string FullName
        {
            get
            {
                if(_timeOfDay >= 6 && _timeOfDay < 9) // 06:00 - 08:59
                {
                    msg = $"Godmorgen { FirstName }";
                }
                else if (_timeOfDay >= 9 && _timeOfDay < 12) // 09:00 - 11:59
                {
                    msg = $"God formiddag { FirstName }";
                }
                else if (_timeOfDay >= 12 && _timeOfDay < 18) // 12:00 - 17:59
                {
                    msg = $"God eftermiddag { FirstName }";
                }
                else if (_timeOfDay >= 18 && _timeOfDay < 24) // 18:00 - 23:59
                {
                    msg = $"Godaften { FirstName }";
                }
                else if (_timeOfDay >= 0 && _timeOfDay < 6) // 00:00 - 05:59
                {
                    msg = $"Natuglen, { FirstName }";
                }

                return msg;
            }
        }

        public MainMenuViewModel(IEventAggregator events)
        {
            _firstName = DashboardViewModel.u.FirstName;
            _lastName = DashboardViewModel.u.LastName;
            _timeOfDay = DateTime.Now.Hour;
            _events = events;
        }

        public void LogOut()
        {
            _events.PublishOnUIThread(new LogOutEvent());
        }
    }
}