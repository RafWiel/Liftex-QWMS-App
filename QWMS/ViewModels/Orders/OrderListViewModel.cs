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

        public ObservableCollection<OrderListModel> Orders { get; } = new();
        public Command GetInitialItemsCommand { get; }
        public Command GetNextItemsCommand { get; }
        public Command GoToDetailsCommand { get; }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set => Set(ref _searchText, value);
        }        

        private IMessageDialogsService _messageDialogsService;
        private IOrdersService _ordersService;
        private IBarcodeReaderService _barcodeReaderService;
        private ILogger<OrderListViewModel> _logger;

        private int _currentPage = 1;

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

            GetInitialItemsCommand = new Command(async (isForced) => await GetInitialItemsAsync((bool)isForced));
            GetNextItemsCommand = new Command(async () => await GetNextItemsAsync());
            GoToDetailsCommand = new Command(async (order) => await GoToDetailsAsync((OrderListModel)order));
        }

        public Task Initialize()
        {            
            _barcodeReaderService.BarcodeReceived += _barcodeReader_BarcodeReceived;

            return GetInitialItemsAsync(false);
        }

        public void Uninitialize()
        {
            _barcodeReaderService.BarcodeReceived -= _barcodeReader_BarcodeReceived;
        }

        private async Task GetInitialItemsAsync(bool isForced)
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

                var orders = await _ordersService.Get(_searchText, null); 
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
                _currentPage = 1;
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

        private async Task GetNextItemsAsync()
        {
            if (IsBusy)
                return;
            
            try
            {
                IsBusy = true;

                var orders = await _ordersService.Get(_searchText, ++_currentPage);
                if (orders == null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _messageDialogsService.ShowError("Błąd aplikacji", "Nieudane pobranie listy zamówień", 3000);
                    });

                    return;
                }
                
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

        private async Task GoToDetailsAsync(OrderListModel order)
        {
            await Shell.Current.GoToAsync(nameof(OrderDetailsPage), true, new Dictionary<string, object>
            {
                { nameof(OrderDetailsViewModel.Model), order }
            });
        }

        private void _barcodeReader_BarcodeReceived(string barcode)
        {
            //_messageDialogsService.ShowNotification("Barcode", barcode, 1500);
        }        
    }
}
