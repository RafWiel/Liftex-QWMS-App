using CommunityToolkit.Maui.Core;
using MetroLog;
using Microsoft.Extensions.Logging;
using QWMS.Enums;
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

        public MessageDialogsService(IPopupService popupService)
        {
            _popupService = popupService;
        }

        public async void ShowNotification(string title, string message, int delay = 0)
        {                      
            if (delay > 0)
            {
                await _popupService.ShowPopupAsync<AutoMessageDialogViewModel>(onPresenting: viewModel => 
                    viewModel.Initialize(title, message, MessageType.Notification, delay)
                );

                return;
            }

            await _popupService.ShowPopupAsync<MessageDialogViewModel>(onPresenting: viewModel => 
                viewModel.Initialize(title, message, MessageType.Notification)
            );
        }

        public async void ShowError(string title, string message, int delay = 0)
        {            
            if (delay > 0)
            {
                await _popupService.ShowPopupAsync<AutoMessageDialogViewModel>(onPresenting: viewModel => 
                    viewModel.Initialize(title, message, MessageType.Error, delay)
                );

                return;
            }

            await _popupService.ShowPopupAsync<MessageDialogViewModel>(onPresenting: viewModel => 
                viewModel.Initialize(title, message, MessageType.Error)
            );
        }
    }
}
