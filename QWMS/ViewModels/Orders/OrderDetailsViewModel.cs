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
    [QueryProperty(nameof(Model), nameof(Model))]
    public class OrderDetailsViewModel : BaseViewModel
    {
        private IMessageDialogsService _messageDialogsService;

        private OrderListModel _model = new();
        public OrderListModel Model
        {
            get => _model;
            set => Set(ref _model, value);
        }

        public OrderDetailsViewModel(IMessageDialogsService messageDialogsService) : base() 
        { 
            _messageDialogsService = messageDialogsService;
        }

        public void ShowMessage()
        {
           _messageDialogsService.ShowNotification("Zamówienie", Model.Name, 1500);
        }

        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }        
    }
}
