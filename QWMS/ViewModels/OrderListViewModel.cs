using QWMS.Models;
using QWMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QWMS.ViewModels
{
    public class OrderListViewModel : BaseViewModel
    {
        public ObservableCollection<OrderListModel> Orders { get; } = new();
        public Command GetOrdersCommand { get; }

        private OrderListService _ordersService;

        public OrderListViewModel(OrderListService ordersService) 
        {
            _ordersService = ordersService;

            GetOrdersCommand = new Command(async () => await GetOrdersAsync());
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
            }
        }
    }
}
