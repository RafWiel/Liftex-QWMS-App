using CommunityToolkit.Maui.Core;
using QWMS.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels
{
    public class PageViewModel : BaseViewModel
    {        
        protected readonly IPopupService _popupService;

        public PageViewModel(IPopupService popupService) 
        {
            _popupService = popupService;
        }
                         
        public async void ShowMessageDialog(string title, string message)
        {
            await _popupService.ShowPopupAsync<MessageDialogViewModel>(onPresenting: viewModel => viewModel.Initialize(title, message));
        }

        public async void ShowAutoMessageDialog(string title, string message, int delay)
        {
            await _popupService.ShowPopupAsync<AutoMessageDialogViewModel>(onPresenting: viewModel => viewModel.Initialize(title, message, delay));
        }        
    }
}
