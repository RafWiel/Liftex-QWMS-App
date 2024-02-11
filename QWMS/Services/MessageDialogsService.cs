using CommunityToolkit.Maui.Core;
using MetroLog;
using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using QWMS.Models.Orders;
using QWMS.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QWMS.Services
{
    public class MessageDialogsService : IMessageDialogsService
    {
        protected readonly IPopupService _popupService;
        private IAudioService _audioService;

        public MessageDialogsService(IPopupService popupService, IAudioService audioService)
        {
            _popupService = popupService;
            _audioService = audioService;                
        }

        public async void ShowNotification(string title, string message, int delay = 0)
        {
            _audioService.PlayNotificationSound();
            za wczesnie, przenies do OnAppearing

            if (delay > 0)
            {
                await _popupService.ShowPopupAsync<AutoMessageDialogViewModel>(onPresenting: viewModel => viewModel.Initialize(title, message, delay));
                return;
            }

            await _popupService.ShowPopupAsync<MessageDialogViewModel>(onPresenting: viewModel => viewModel.Initialize(title, message));
        }

        public async void ShowError(string title, string message, int delay = 0)
        {
            _audioService.PlayErrorSound();

            if (delay > 0)
            {
                await _popupService.ShowPopupAsync<AutoMessageDialogViewModel>(onPresenting: viewModel => viewModel.Initialize(title, message, delay));
                return;
            }

            await _popupService.ShowPopupAsync<MessageDialogViewModel>(onPresenting: viewModel => viewModel.Initialize(title, message));
        }
    }
}
