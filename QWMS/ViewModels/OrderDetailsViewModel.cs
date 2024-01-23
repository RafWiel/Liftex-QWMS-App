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
    [QueryProperty(nameof(Order), nameof(Order))]
    public partial class OrderDetailsViewModel : BaseViewModel
    {
        private OrderModel _order = new();
        public OrderModel Order
        {
            get => _order;
            set => Set(ref _order, value);
        }

        public async Task ShowMessage()
        {
            await Shell.Current.DisplayAlert("Zamówienie", Order.Name, "OK");
        }

        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        
    }
}
