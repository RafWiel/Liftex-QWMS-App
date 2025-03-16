﻿using MetroLog;
using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using QWMS.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QWMS.Helpers;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using QWMS.DataTransferObjects;

namespace QWMS.Services
{
    public class OrdersService : IOrdersService
    {        
        private HttpClient _httpClient;
        private ILogger<OrdersService> _logger;
        private IConfiguration _configuration;

        public OrdersService(ILogger<OrdersService> logger, IConfiguration configuration) 
        { 
            _logger = logger;
            _configuration = configuration;

            _httpClient = new HttpClient();
        }

        public async Task<List<OrderListModel>?> Get(string? search, int? page)
        {
            try
            {
                var query = new Dictionary<string, string?>
                {
                    { "search", search },
                    { "page", page?.ToString() },
                };
                
                var response = await _httpClient.GetAsync(Tools.BuildUrl($"{_configuration.ApiUrl}/v1/orders", query));
                if (!response.IsSuccessStatusCode)
                    return null;

                var models = await response.Content.ReadFromJsonAsync<List<OrderListModel>>();

                //using var stream = await FileSystem.OpenAppPackageFileAsync("Orders.json");
                //using var reader = new StreamReader(stream);
                //var contents = await reader.ReadToEndAsync();
                //_orders = JsonSerializer.Deserialize<List<OrderModel>>(contents) ?? new();

                //Thread.Sleep(1000);

                return models;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);                
            }

            return null;
        }

        public async Task<string> Test()
        {
            try
            {                
                //naglowek
                var response = await _httpClient.PostAsync(
                    $"{_configuration.ApiUrl}/v1/orders/test/header", 
                    null);

                if (!response.IsSuccessStatusCode)
                    return "Nieudane utworzenie nagłówka";

                var responseDto = await response.Content.ReadFromJsonAsync<IdResponseDto>();

                var requestDto = new OrderDto
                {
                    Id = responseDto!.Id,                    
                };

                //towar
                var content = new StringContent(
                    JsonConvert.SerializeObject(requestDto), 
                    Encoding.UTF8, 
                    "application/json");

                response = await _httpClient.PostAsync(
                    $"{_configuration.ApiUrl}/v1/orders/test/item", 
                    content);

                if (!response.IsSuccessStatusCode)
                    return "Nieudane dodanie pozycji towaru";

                //zamkniecie zamowienia
                response = await _httpClient.PostAsync(
                    $"{_configuration.ApiUrl}/v1/orders/test/close", 
                    content);

                if (!response.IsSuccessStatusCode)
                    return "Nieudane zamknięcie zamówienia";

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return "Wystąpił niekreślony błąd";
        }
    }
}
