using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using QWMS.Models.Barcodes;
using QWMS.Views.Barcodes;
using System.Collections.ObjectModel;

namespace QWMS.ViewModels.Barcodes
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    [QueryProperty(nameof(ProductCode), nameof(ProductCode))]
    public class BarcodeListViewModel : BaseViewModel
    {
        private DateTime _refreshTimestamp;

        public ObservableCollection<BarcodeListModel> Barcodes { get; } = new();
        public Command GetInitialItemsCommand { get; }
        public Command GetNextItemsCommand { get; }        

        private IMessageDialogsService _messageDialogsService;
        private IBarcodesService _barcodesService;
        private ILogger<BarcodeListViewModel> _logger;

        private int _currentPage = 1;

        #region Properties        

        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;

        private string _title = "Kody kreskowe";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        public BarcodeListViewModel(
            IMessageDialogsService messageDialogsService,
            IBarcodesService barcodesService,             
            ILogger<BarcodeListViewModel> logger) : base()
        {
            _messageDialogsService = messageDialogsService;
            _barcodesService = barcodesService;
            _logger = logger;

            GetInitialItemsCommand = new Command(async (isForced) => await GetInitialItemsAsync((bool)isForced));
            GetNextItemsCommand = new Command(async (isForced) => await GetNextItemsAsync());            
        }

        public Task Initialize()
        {
            Title = ProductCode;

            return GetInitialItemsAsync(true);            
        }

        public void Uninitialize()
        {            
        }

        private async Task GetInitialItemsAsync(bool isForced)
        {
            if (IsBusy)
                return;

            if (!isForced &&
                Barcodes.Count > 0 &&
                (DateTime.Now - _refreshTimestamp).TotalMinutes < 1)
                return;

            try
            {
                IsBusy = true;

                var barcodes = await _barcodesService.Get(ProductId, null);
                if (barcodes == null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _messageDialogsService.ShowError("Błąd aplikacji", "Nieudane pobranie listy kodów kreskowych", 3000);
                    });

                    return;
                }

                if (Barcodes.Count > 0)
                    Barcodes.Clear();

                foreach (var barcode in barcodes)
                    Barcodes.Add(barcode);

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

                var barcodes = await _barcodesService.Get(ProductId, ++_currentPage);
                if (barcodes == null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _messageDialogsService.ShowError("Błąd aplikacji", "Nieudane pobranie listy kodów kreskowych", 3000);
                    });

                    return;
                }

                foreach (var barcode in barcodes)
                    Barcodes.Add(barcode);
                
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
    }
}
