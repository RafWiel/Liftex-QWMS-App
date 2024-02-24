using MetroLog;
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

namespace QWMS.Services
{
    public class OrdersService : IOrdersService
    {        
        private HttpClient _httpClient;
        private ILogger<OrdersService> _logger;

        public OrdersService(ILogger<OrdersService> logger) 
        { 
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<List<OrderListModel>?> Get()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://192.168.1.110:3001/api/v1/orders");
                if (!response.IsSuccessStatusCode)
                    return null;

                var orders = await response.Content.ReadFromJsonAsync<List<OrderListModel>>();

                //using var stream = await FileSystem.OpenAppPackageFileAsync("Orders.json");
                //using var reader = new StreamReader(stream);
                //var contents = await reader.ReadToEndAsync();
                //_orders = JsonSerializer.Deserialize<List<OrderModel>>(contents) ?? new();

                //Thread.Sleep(1000);

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);                
            }

            return null;
        }
    }
}
