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
    public class BaseDialogViewModel : BaseViewModel
    {
        public delegate void CloseDelegate();
        public event CloseDelegate? CloseEvent;

        private IAudioService _audioService;
        protected MessageType MessageType { get; set; }
        
        public BaseDialogViewModel(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public void PlayInitialSound()
        {
            switch (MessageType)
            {
                case MessageType.Notification:
                    _audioService.PlayNotificationSound();
                    break;

                case MessageType.Error:
                    _audioService.PlayErrorSound();
                    break;
            }
        }

        protected void InvokeCloseEvent()
        {
            CloseEvent?.Invoke();
        }
    }
}


