using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using QWMS.Models.Reservations;
using QWMS.Views.Reservations;
using System.Collections.ObjectModel;

namespace QWMS.ViewModels.Reservations
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    [QueryProperty(nameof(ProductCode), nameof(ProductCode))]
    public class ReservationListViewModel : BaseViewModel
    {
        private DateTime _refreshTimestamp;

        public ObservableCollection<ReservationListModel> Reservations { get; } = new();
        public Command GetInitialItemsCommand { get; }
        public Command GetNextItemsCommand { get; }        

        private IMessageDialogsService _messageDialogsService;
        private IReservationsService _reservationsService;
        private ILogger<ReservationListViewModel> _logger;

        private int _currentPage = 1;

        #region Properties        

        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;

        private string _title = "Rezerwacje";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        public ReservationListViewModel(
            IMessageDialogsService messageDialogsService,
            IReservationsService reservationsService,             
            ILogger<ReservationListViewModel> logger) : base()
        {
            _messageDialogsService = messageDialogsService;
            _reservationsService = reservationsService;
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
                Reservations.Count > 0 &&
                (DateTime.Now - _refreshTimestamp).TotalMinutes < 1)
                return;

            try
            {
                IsBusy = true;

                var reservations = await _reservationsService.Get(ProductId, null);
                if (reservations == null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _messageDialogsService.ShowError("Błąd aplikacji", "Nieudane pobranie listy rezerwacji", 3000);
                    });

                    return;
                }

                if (Reservations.Count > 0)
                    Reservations.Clear();

                foreach (var reservation in reservations)
                    Reservations.Add(reservation);

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

                var reservations = await _reservationsService.Get(ProductId, ++_currentPage);
                if (reservations == null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _messageDialogsService.ShowError("Błąd aplikacji", "Nieudane pobranie listy rezerwacji", 3000);
                    });

                    return;
                }

                foreach (var reservation in reservations)
                    Reservations.Add(reservation);
                
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
