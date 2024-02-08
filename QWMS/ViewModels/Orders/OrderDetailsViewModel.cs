using CommunityToolkit.Maui.Core;
using QWMS.Models;
using QWMS.Services;
using QWMS.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Orders
{
    [QueryProperty(nameof(Order), nameof(Order))]
    public class OrderDetailsViewModel : BaseViewModel
    {
        private readonly IPopupService _popupService;

        private OrderModel _order = new();
        public OrderModel Order
        {
            get => _order;
            set => Set(ref _order, value);
        }

        public OrderDetailsViewModel(IPopupService popupService)
        {
            _popupService = popupService;
        }

        public void ShowMessage()
        {
            ShowAutoMessageDialog("Zamówienie", Order.Name, 1500);
        }

        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
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
