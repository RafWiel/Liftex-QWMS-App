﻿using Com.Cipherlab.Barcode.Decoderparams;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using QWMS.Models.Products;
using QWMS.Services;
using QWMS.ViewModels.Dialogs;
using QWMS.Views.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Products
{
    public class ProductListViewModel : BaseViewModel
    {
        private DateTime _refreshTimestamp;

        public ObservableCollection<ProductListModel> Products { get; } = new();
        public Command GetProductsCommand { get; }
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

        private IMessageDialogsService _messageDialogsService;
        private IProductsService _productsService;
        private IBarcodeReaderService _barcodeReaderService;
        private ILogger<ProductListViewModel> _logger;

        public ProductListViewModel(
            IMessageDialogsService messageDialogsService,
            IProductsService productsService, 
            IBarcodeReaderService barcodeReaderService,
            ILogger<ProductListViewModel> logger) : base()
        {
            _messageDialogsService = messageDialogsService;
            _productsService = productsService;
            _barcodeReaderService = barcodeReaderService; 
            _logger = logger;   

            GetProductsCommand = new Command(async (isForced) => await GetProductsAsync((bool)isForced));
            GoToDetailsCommand = new Command(async (order) => await GoToDetailsAsync((ProductListModel)order));
        }

        public void Initialize()
        {            
            _barcodeReaderService.BarcodeReceived += _barcodeReader_BarcodeReceived;
        }

        public void Uninitialize()
        {
            _barcodeReaderService.BarcodeReceived -= _barcodeReader_BarcodeReceived;
        }

        private async Task GetProductsAsync(bool isForced)
        {
            if (IsBusy)
                return;

            if (!isForced &&
                Products.Count > 0 &&
                (DateTime.Now - _refreshTimestamp).TotalMinutes < 1)
                return;

            try
            {
                IsBusy = true;

                var products = await _productsService.Get();
                if (products == null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _messageDialogsService.ShowError("Błąd aplikacji", "Nieudane pobranie listy towarów", 3000);
                    });

                    return;
                }

                if (Products.Count > 0)
                    Products.Clear();

                foreach (var product in products)
                    Products.Add(product);

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

        private async Task GoToDetailsAsync(ProductListModel product)
        {
            await Shell.Current.GoToAsync(nameof(ProductDetailsPage), true, new Dictionary<string, object>
            {
                { nameof(ProductDetailsViewModel.Model), product }
            });
        }

        private void _barcodeReader_BarcodeReceived(string barcode)
        {
            _messageDialogsService.ShowNotification("Barcode", barcode, 1500);
        }        
    }
}