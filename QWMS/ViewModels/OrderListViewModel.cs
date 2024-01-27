using Com.Cipherlab.Barcode.Decoderparams;
using CommunityToolkit.Maui.Core;
using QWMS.Models;
using QWMS.Services;
using QWMS.ViewModels.Dialogs;
using QWMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QWMS.ViewModels
{
    public partial class OrderListViewModel : BaseViewModel
    {
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

        private OrderListService _ordersService;
        private BarcodeReaderService _barcodeReaderService;
        private readonly IPopupService _popupService;

        public OrderListViewModel(OrderListService ordersService, BarcodeReaderService barcodeReaderService, IPopupService popupService) 
        {
            _ordersService = ordersService;
            _barcodeReaderService = barcodeReaderService;
            _popupService = popupService;
            
            GetOrdersCommand = new Command(async () => await GetOrdersAsync());    
            GoToDetailsCommand = new Command(async (Object order) => await GoToDetailsAsync((OrderModel)order));
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

        async Task GetOrdersAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var orders = await _ordersService.GetOrders();
                if (Orders.Count > 0)
                    Orders.Clear();

                foreach (var order in orders)
                    Orders.Add(order);                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                IsBusy = false;
                //IsRefreshing = false;
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
            ShowAutoMessageDialog("Barcode", barcode, 1500);            
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
