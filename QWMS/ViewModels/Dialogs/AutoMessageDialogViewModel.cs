using QWMS.Enums;
using QWMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Dialogs
{
    public class AutoMessageDialogViewModel : BaseDialogViewModel
    {                
        private IDispatcherTimer? _timer;
        
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

        public AutoMessageDialogViewModel(IAudioService audioService) : base(audioService) 
        {           
            var app = Application.Current;
            if (app == null)
                return;

            _timer = app.Dispatcher.CreateTimer();            
            _timer.Tick += (s, e) => InvokeCloseEvent();            
        }

        public void Initialize(string title, string message, MessageType messageType, int delay)
        { 
            Title = title;
            Message = message;
            MessageType = messageType;
            Delay = delay;            
        }

        public void Start()
        {
            _timer?.Start();
        }

        public void Stop()
        {
            _timer?.Stop();
        }        
    }
}


