using QWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QWMS.Services
{
    public class OrderListService
    {
        List<OrderListModel> _orders = new();
        HttpClient _httpClient;

        public OrderListService() 
        { 
            _httpClient = new HttpClient();
        }

        public async Task<List<OrderListModel>> GetOrders()
        {
            if (_orders.Count > 0)
                return _orders;

            //var response = await _httpClient.GetAsync("https://xxx/orders.json");
            //if (response.IsSuccessStatusCode)
            //{
            //    _orders = await response.Content.ReadFromJsonAsync<List<OrderListModel>>();
            //}

            using var stream = await FileSystem.OpenAppPackageFileAsync("Orders.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            _orders = JsonSerializer.Deserialize<List<OrderListModel>>(contents);

            return _orders;
        }
    }
}
