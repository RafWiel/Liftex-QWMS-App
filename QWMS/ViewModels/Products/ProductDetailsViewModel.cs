using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using QWMS.Models.Products;
using QWMS.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Products
{
    public class ProductDetailsViewModel : PageViewModel
    {
        #region Initialization
        
        private ProductsService _productsService;
        private BarcodeReaderService _barcodeReaderService;
        private ILogger<ProductDetailsViewModel> _logger;

        public ProductDetailsViewModel(
            ProductsService productsService, 
            BarcodeReaderService barcodeReaderService,
            ILogger<ProductDetailsViewModel> logger,
            IPopupService popupService) : base(popupService) 
        { 
            _productsService = productsService;
            _barcodeReaderService = barcodeReaderService;
            _logger = logger;

            TODO: rozmiar czcionki i beep
        }

        #endregion

        #region Properties 

        private string _title = "Kod towaru";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private ProductModel _model = new();
        public ProductModel Model
        {
            get => _model;
            set
            {
                Set(ref _model, value);
                NotifyPropertyChanged(nameof(PriceStr));
                NotifyPropertyChanged(nameof(CountStr));
            }
        }
        
        public string PriceStr => _model.Price > 0 ? $"{_model.Price:0.00}PLN" : string.Empty;        
        public string CountStr => _model.Count > 0 ? _model.Count.ToString(CultureInfo.InvariantCulture) : string.Empty;

        #endregion

        #region Events

        private async void _barcodeReader_BarcodeReceived(string barcode)
        {
            await GetProductAsync(barcode);         
        }

        #endregion

        #region Methods

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

        public void ShowScanMessage()
        {
            ShowAutoMessageDialog("Skanowanie", "Proszę zeskanować kod kreskowy towaru", 1500);
        }

        private async Task GetProductAsync(string barcode)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var model = await _productsService.GetOne(barcode);
                if (model == null)
                {
                    ShowAutoMessageDialog("Błąd aplikacji", "Nie znaleziono towaru o podanym kodzie", 3000);
                    return;
                }

                Model = model;
                Title = model.Code;
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

        #endregion
    }
}
