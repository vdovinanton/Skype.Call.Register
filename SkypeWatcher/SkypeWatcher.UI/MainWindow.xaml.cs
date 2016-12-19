using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using SkypeWatcher.Core.Mock;
using SkypeWatcher.Entity;
using SkypeWatcher.Entity.Models;
using SKYPE4COMLib;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace SkypeWatcher.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // For ringing animation
        private int _count;

        // For time TexBlock
        private TimeSpan _timeAgo;

        // For the temp values Time.Content
        private double _tempSpeakCount = 0;

        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private readonly DispatcherTimer _ringing = new DispatcherTimer();
        private readonly Core.SkypeWatcher _skype = new Core.SkypeWatcher();

        public MainWindow()
        {
            InitializeComponent();
            // Hide and to right low rectangle
            StartLocation();

            StartActions();
            StartSubscrubes();
        }

        private void CallInProgress(object sender, ViewModel vm)
        {
            NickName.Text = vm.Name;

            if (vm.Status == TCallStatus.clsRinging)
            {
                CallingAnimationStart();
            }
            else
            {
                CallingAnimationStop();
            }

            if (_skype.Billing != null)
            {
                _skype.Billing.OnMinuteLeft += OnMinuteLeft;
            }

            //switch (vm.Status)
            //{
            //    case TCallStatus.clsInProgress: TimerStart(); break;
            //    case TCallStatus.clsFinished: TimerStop(); break;
            //}

            Show();
        }

        private void StartSubscrubes()
        {
            // Logout Icon
            ExitIconBorder.MouseEnter += ExitIconOnMouseEnter;
            ExitIconBorder.MouseLeave += ExitIconOnMouseLeave;
            ExitIconBorder.MouseLeftButtonUp += ExitIconOnMouseLeftButtonUp;

            // Minimaze Icon
            WebIconBorder.MouseEnter += WebIconOnMouseEnter;
            WebIconBorder.MouseLeave += WebIconOnMouseLeave;
            WebIconBorder.MouseLeftButtonUp += WebIconOnMouseLeftButtonUp;

            _skype.CallInProgress += CallInProgress;
            _skype.CallFinished += CallFinished;
            _timer.Tick += TimerTick;
            _ringing.Tick += RingingOnTick;
            _timer.Interval = new TimeSpan(0, 0, 0, 1);
            _ringing.Interval = new TimeSpan(0, 0, 0, 1);
        }

        private void CallFinished(object sender, Dialog dialog)
        {
            ShowingTypeDialogIcon(PaymentType.ByMinute);
            ShowingTypeDialogIcon(PaymentType.ByTimeLimit);
            NoAvalibleUserName();
        }

        /*
        * This methood called bi minute (fo the debug by second)
        * You can change time period following path:
        * SkypeWatcher.Core/SkypeBilling => _period
        */
        private void OnMinuteLeft(object sender, WebUser e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background,
              new Action(() =>
              {
                  if (e.DialogType == PaymentType.ByTimeLimit)
                  {
                      // Show current time and time limit
                      if (Math.Abs(_tempSpeakCount) < 1)
                      {
                          // +1 then when firts event calling count already -1
                          _tempSpeakCount = e.LimitTimeValue + 1;
                      }
                      ShowingTypeDialogIcon(e.DialogType, true);
                      Time.Content = $"{DateTime.Now.ToString("mm:ss")}/{_tempSpeakCount}";
                  }
                  if (e.DialogType == PaymentType.ByMinute)
                  {
                      // Show payment speak value 
                      ShowingTypeDialogIcon(e.DialogType, true);
                      _tempSpeakCount = _tempSpeakCount + e.TariffPlan;
                      Time.Content = _tempSpeakCount.ToString("##.##");
                  }
              }));
        }

        private void StartLocation()
        {
            Topmost = true;
            var primaryMonitorArea = SystemParameters.WorkArea;
            Left = primaryMonitorArea.Right - Width - 10;
            Top = primaryMonitorArea.Bottom - Height - 10;
        }

        private void StartActions()
        {
            Hide();
            ShowingTypeDialogIcon(PaymentType.ByMinute);
            ShowingTypeDialogIcon(PaymentType.ByTimeLimit);
            CallInProgress1.Visibility = Visibility.Hidden;
            CallInProgress2.Visibility = Visibility.Hidden;
            NoAvalibleUserName();

            var notify = new NotifyIcon
            {
                Icon = Properties.Resources.skype_gr,
                Visible = true
            };

            notify.DoubleClick += NotifyOnDoubleClick;
        }

        private void CallingAnimationStop()
        {
            _ringing.Stop();
            CallInProgress1.Visibility = Visibility.Hidden;
            CallInProgress2.Visibility = Visibility.Hidden;
        }

        private void ShowingTypeDialogIcon(PaymentType type, bool isShow = false)
        {
            if (type == PaymentType.ByTimeLimit)
            {
                if (isShow)
                {
                    Time.Visibility = Visibility.Visible;
                    IconTime.Visibility = Visibility.Visible;
                }
                else
                {
                    Time.Visibility = Visibility.Hidden;
                    IconTime.Visibility = Visibility.Hidden;
                }
            }

            if (type == PaymentType.ByMinute)
            {
                if (isShow)
                {
                    Time.Visibility = Visibility.Visible;
                    DoallarIcon.Visibility = Visibility.Visible;
                }
                else
                {
                    Time.Visibility = Visibility.Hidden;
                    DoallarIcon.Visibility = Visibility.Hidden;
                }
            }
        }

        private void CallingAnimationStart()
        {
            _timeAgo = TimeSpan.FromSeconds(1);
            _ringing.Start();
        }

        private void NoAvalibleUserName()
        {
            NickName.Text = "No avalible user";
        }


        private void TimerStart()
        {
            _timer.Start();
            Time.Visibility = Visibility.Visible;
            IconTime.Visibility = Visibility.Visible;
        }

        private void TimerStop()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                Time.Visibility = Visibility.Hidden;
                IconTime.Visibility = Visibility.Hidden;
                NoAvalibleUserName();
            }
        }

        #region Events
        private void NotifyOnDoubleClick(object sender, EventArgs eventArgs)
        {
            Show();
            WindowState = WindowState.Normal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();

            base.OnStateChanged(e);
        }

        private void RingingOnTick(object sender, EventArgs eventArgs)
        {
            _count++;
            if (_count % 2 == 0)
            {
                CallInProgress2.Visibility = Visibility.Visible;
                CallInProgress1.Visibility = Visibility.Hidden;
            }
            else
            {
                CallInProgress1.Visibility = Visibility.Visible;
                CallInProgress2.Visibility = Visibility.Hidden;
            }
        }

        private void TimerTick(object sender, EventArgs eventArgs)
        {
            _timeAgo += TimeSpan.FromSeconds(1);
            //Time.Content = _timeAgo;
        }

        #region Button Events
        private void WebIconOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            Hide();
        }

        private void ExitIconOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void WebIconOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            WebIcon.Foreground = Brushes.AliceBlue;
        }

        private void WebIconOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            WebIcon.Foreground = Brushes.CadetBlue;
        }

        private void ExitIconOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            ExitIcon.Foreground = Brushes.AliceBlue;
        }

        private void ExitIconOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            ExitIcon.Foreground = Brushes.CadetBlue;
        }
        #endregion

        #endregion
    }
}
