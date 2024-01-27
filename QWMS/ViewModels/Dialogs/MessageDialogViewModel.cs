using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Dialogs
{
    public class MessageDialogViewModel : BaseViewModel
    {
        public delegate void CloseDelegate();
        public event CloseDelegate? CloseEvent;

        public Command CloseCommand { get; }

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

        public MessageDialogViewModel()
        {
            CloseCommand = new Command(() =>
            {
                CloseEvent?.Invoke();
            });
        }

        public void Initialize(string title, string message)
        { 
            Title = title;
            Message = message;
        }        
    }
}


