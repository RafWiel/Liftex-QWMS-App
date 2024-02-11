using Com.Cipherlab.Barcode.Decoderparams;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using QWMS.Models.Orders;
using QWMS.Services;
using QWMS.ViewModels.Dialogs;
using QWMS.Views.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Orders
{
    public class OrderListViewModel : BaseViewModel
    {
        private DateTime _refreshTimestamp;

        public ObservableCollection<OrderModel> Orders { get; } = new();
        public Command GetOrdersCommand { get; }
        public Command GoToDetailsCommand { get; }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set => Set(ref _searchText, value);
        }

        //private bool _isRefreshing;
        //public bool IsRefreshing
        //{
        //    get => _isRefreshing;
        //    set => Set(ref _isRefreshing, value);
        //}

        private IMessageDialogsService _messageDialogsService;
        private IOrdersService _ordersService;
        private IBarcodeReaderService _barcodeReaderService;
        private ILogger<OrderListViewModel> _logger;

        public OrderListViewModel(
            IMessageDialogsService messageDialogsService,
            IOrdersService ordersService, 
            IBarcodeReaderService barcodeReaderService,
            ILogger<OrderListViewModel> logger) : base()
        {
            _messageDialogsService = messageDialogsService;
            _ordersService = ordersService;
            _barcodeReaderService = barcodeReaderService; 
            _logger = logger;   

            GetOrdersCommand = new Command(async (isForced) => await GetOrdersAsync((bool)isForced));
            GoToDetailsCommand = new Command(async (order) => await GoToDetailsAsync((OrderModel)order));
        }

        public void Initialize()
        {
            //if (Device.RuntimePlatform == Device.Android)

#if ANDROID
            _barcodeReaderService.BarcodeReceived += _barcodeReader_BarcodeReceived;
#endif
        }

        public void Uninitialize()
        {
#if ANDROID
            _barcodeReaderService.BarcodeReceived -= _barcodeReader_BarcodeReceived;
#endif
        }

        private async Task GetOrdersAsync(bool isForced)
        {
            if (IsBusy)
                return;

            if (!isForced &&
                Orders.Count > 0 &&
                (DateTime.Now - _refreshTimestamp).TotalMinutes < 1)
                return;

            try
            {
                IsBusy = true;

                var orders = await _ordersService.GetOrders();
                if (orders == null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _messageDialogsService.ShowError("Błąd aplikacji", "Nieudane pobranie listy zamówień", 3000);
                    });

                    return;
                }

                if (Orders.Count > 0)
                    Orders.Clear();

                foreach (var order in orders)
                    Orders.Add(order);

                _refreshTimestamp = DateTime.Now;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GoToDetailsAsync(OrderModel order)
        {
            await Shell.Current.GoToAsync(nameof(OrderDetailsPage), true, new Dictionary<string, object>
            {
                { nameof(OrderDetailsViewModel.Order), order }
            });
        }

        private void _barcodeReader_BarcodeReceived(string barcode)
        {
            _messageDialogsService.ShowNotification("Barcode", barcode, 1500);
        }

        //public async void ShowMessageDialog(string title, string message)
        //{
        //    await _popupService.ShowPopupAsync<MessageDialogViewModel>(onPresenting: viewModel => viewModel.Initialize(title, message));
        //}

        //public async void ShowAutoMessageDialog(string title, string message, int delay)
        //{
        //    await _popupService.ShowPopupAsync<AutoMessageDialogViewModel>(onPresenting: viewModel => viewModel.Initialize(title, message, delay));
        //}
    }
}
