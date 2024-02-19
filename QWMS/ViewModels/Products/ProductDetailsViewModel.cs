using Com.Cipherlab.Barcode.Decoderparams;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using QWMS.Models.Products;
using QWMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Products
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        #region Initialization

        private IMessageDialogsService _messageDialogsService;
        private IProductsService _productsService;
        private IBarcodeReaderService _barcodeReaderService;
        private IAudioService _audioService;
        private ILogger<ProductDetailsViewModel> _logger;

        public Command SearchCommand { get; }
        public Command ShowEanCodesCommand { get; }
        
        public ObservableCollection<ProductDetailsCountModel> Items { get; } = new();

        public ProductDetailsViewModel(
            ILogger<ProductDetailsViewModel> logger,
            IMessageDialogsService messageDialogsService,
            IProductsService productsService, 
            IBarcodeReaderService barcodeReaderService,           
            IAudioService audioService) : base()
        {
            _logger = logger;
            _messageDialogsService = messageDialogsService;
            _productsService = productsService;
            _barcodeReaderService = barcodeReaderService;
            _audioService = audioService;            

            SearchCommand = new Command(async () => await SearchAsync());
            ShowEanCodesCommand = new Command(async () => await ShowEanCodesAsync());            
        }

        #endregion

        #region Properties 

        private string _title = "Towar";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private bool _isProductLoaded;
        public bool IsProductLoaded
        {
            get => _isProductLoaded;
            set => Set(ref _isProductLoaded, value);
        }

        private ProductDetailsModel _model = new();
        public ProductDetailsModel Model
        {
            get => _model;
            set
            {
                Set(ref _model, value);
                NotifyPropertyChanged(nameof(PriceStr));
                NotifyPropertyChanged(nameof(CountStr));


                IsProductLoaded = true;
            }
        }

        public string PriceStr => _model.Price.ToString("0.00", CultureInfo.InvariantCulture);
        public string CountStr => _model.Count.ToString("0.#", CultureInfo.InvariantCulture);

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
            _barcodeReaderService.BarcodeReceived += _barcodeReader_BarcodeReceived;            
        }

        public void Uninitialize()
        {
            _barcodeReaderService.BarcodeReceived -= _barcodeReader_BarcodeReceived;            
        }

        public async void ShowScanMessage()
        {
            _messageDialogsService.ShowNotification("Skanowanie", "Proszę zeskanować kod kreskowy towaru", 1500);

            await GetProductAsync("2010000000014");

            //TODO: MeasureUnitDecimalPlaces na liscie i zastepczy tekst przy wgrywaniu pozycji (puste glupio wyglada)
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
                    _messageDialogsService.ShowError("Błąd aplikacji", "Nie znaleziono towaru o podanym kodzie", 3000);
                    return;
                }

                Model = model;
                Title = model.Name;
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

        private async Task SearchAsync()
        {
            _messageDialogsService.ShowNotification("Skanowanie", "Proszę zeskanować kod kreskowy towaru", 1500);
            //_audioService.PlayBeepSound();
        }

        private async Task ShowEanCodesAsync()
        {
            _audioService.PlayErrorSound();
        }

        #endregion
    }
}
