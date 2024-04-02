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
    public class CallbackMessageDialogViewModel : BaseDialogViewModel
    {                
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

        public CallbackMessageDialogViewModel(IAudioService audioService) : base(audioService) 
        {            
            CloseCommand = new Command(() =>
            {
                InvokeCloseEvent();
            });            
        }

        public CallbackMessageDialogViewModel Initialize(string title, string message, MessageType messageType)
        { 
            Title = title;
            Message = message;
            MessageType = messageType;

            return this;
        }        
    }
}


