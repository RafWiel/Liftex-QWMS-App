using Com.Cipherlab.Barcode.Decoderparams;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using QWMS.Models.Products;
using QWMS.Services;
using QWMS.ViewModels.Barcodes;
using QWMS.Views.Barcodes;
using QWMS.Views.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Products
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    public class ProductDetailsViewModel : BaseViewModel
    {
        #region Initialization

        private IMessageDialogsService _messageDialogsService;
        private IProductsService _productsService;
        private IBarcodeReaderService _barcodeReaderService;
        private IAudioService _audioService;
        private ILogger<ProductDetailsViewModel> _logger;

        public Command GoToListCommand { get; }
        public Command GoToEanCodesCommand { get; }
        public Command GoToReservationsCommand { get; }
        
        //public ObservableCollection<ProductDetailsCountModel> Items { get; } = new();

        #region Properties 

        public int ProductId { get; set; } = 0;

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
                
                Title = _model.Code;
                IsProductLoaded = true;

                if (_model.Id > 0)
                    ProductId = _model.Id;
            }
        }

        #endregion

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

            GoToListCommand = new Command(async () => await GoToListAsync());
            GoToEanCodesCommand = new Command(async () => await GoToEanCodesAsync());
            GoToReservationsCommand = new Command(async () => await GoToReservationsAsync());
        }

        #endregion        

        #region Events

        private async void _barcodeReader_BarcodeReceived(string barcode)
        {
            await GetProductAsync(null, barcode);         
        }

        #endregion

        #region Methods

        public Task Initialize()
        {
            //if (Device.RuntimePlatform == Device.Android)
            _barcodeReaderService.BarcodeReceived += _barcodeReader_BarcodeReceived;

            Model = new();
            Title = "Towar";
            IsProductLoaded = false;

            if (ProductId == 0)
            {
                ShowScanMessage();
                return null;
            }

            return GetProductAsync(ProductId, null);
        }

        public void Uninitialize()
        {
            _barcodeReaderService.BarcodeReceived -= _barcodeReader_BarcodeReceived;            
        }

        private void ShowScanMessage()
        {
            _messageDialogsService.ShowNotification("Skanowanie", "Proszę zeskanować kod kreskowy towaru", 1500);            
        }        

        private async Task GetProductAsync(int? id, string? barcode)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                ProductDetailsModel? model = null;

                if (id != null)
                    model = await _productsService.GetSingle(id.Value);

                if (barcode != null)
                    model = await _productsService.GetSingle(barcode);

                if (model == null)
                {
                    _messageDialogsService.ShowError("Błąd aplikacji", "Nie znaleziono towaru", 3000);
                    return;
                }

                Model = model;                
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

        private Task GoToListAsync()
        {
            //return Shell.Current.GoToAsync($"//{nameof(ProductListPage)}", true);
            return Shell.Current.GoToAsync(nameof(ProductListPage), true);
        }

        private Task GoToEanCodesAsync()
        {
            return Shell.Current.GoToAsync(nameof(BarcodeListPage), true, new Dictionary<string, object>            
            {
                { nameof(BarcodeListViewModel.ProductId), ProductId },
                { nameof(BarcodeListViewModel.ProductCode), Model.Code }
            });
        }

        private async Task GoToReservationsAsync()
        {
            _audioService.PlayErrorSound();
        }

        #endregion
    }
}
