using CommunityToolkit.Maui.Core;
using QWMS.Interfaces;
using QWMS.Models.Orders;
using QWMS.Services;
using QWMS.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Orders
{
    [QueryProperty(nameof(Order), nameof(Order))]
    public class OrderDetailsViewModel : BaseViewModel
    {
        private IMessageDialogsService _messageDialogsService;

        private OrderModel _order = new();
        public OrderModel Order
        {
            get => _order;
            set => Set(ref _order, value);
        }

        public OrderDetailsViewModel(MessageDialogsService messageDialogsService) : base() 
        { 
            _messageDialogsService = messageDialogsService;
        }

        public void ShowMessage()
        {
            _messageDialogsService.ShowNotification("Zamówienie", Order.Name, 1500);
        }

        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }        
    }
}
