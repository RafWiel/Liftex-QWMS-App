using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Dialogs
{
    public class AutoMessageDialogViewModel : BaseViewModel
    {
        public delegate void CloseDelegate();
        public event CloseDelegate? CloseEvent;

        private IDispatcherTimer _timer;

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public int Delay
        {
            get => (int)_timer.Interval.TotalSeconds;
            set => _timer.Interval = new TimeSpan(0, 0, 0, 0, value);
        }

        public AutoMessageDialogViewModel()
        {
            _timer = Application.Current.Dispatcher.CreateTimer();            
            _timer.Tick += (s, e) => CloseEvent?.Invoke();
            
        }

        public void Initialize(string title, string message, int delay)
        { 
            Title = title;
            Message = message;
            Delay = delay;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }        
    }
}


