using QWMS.Models;
using QWMS.Services;
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

        public OrderListViewModel(OrderListService ordersService) 
        {
            _ordersService = ordersService;

            GetOrdersCommand = new Command(async () => await GetOrdersAsync());    
            GoToDetailsCommand = new Command(async (Object order) => await GoToDetailsAsync((OrderModel)order));
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
        
        async Task GoToDetailsAsync(OrderModel order)
        {
            await Shell.Current.GoToAsync(nameof(OrderDetailsPage), true, new Dictionary<string, object>
            {
                { nameof(OrderDetailsViewModel.Order), order }
            });
        }
    }
}
