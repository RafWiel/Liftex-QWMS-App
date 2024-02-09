using MetroLog;
using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using QWMS.Models.Orders;
using QWMS.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace QWMS.Services
{
    public class ProductsService : IProductsService
    {        
        private HttpClient _httpClient;
        private ILogger<ProductsService> _logger;
        int index = 1;

        public ProductsService(ILogger<ProductsService> logger) 
        { 
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<ProductModel?> GetOne(string ean)
        {            
            try
            {
                var query = new Dictionary<string, string>
                {
                    { "ean", ean }
                };
               
                var response = await _httpClient.GetAsync(BuildUrl("http://192.168.1.110:3001/api/v1/products", query));
                if (!response.IsSuccessStatusCode)
                    return null;

                var model = await response.Content.ReadFromJsonAsync<ProductModel>();

                //var model = await Task.Run(() =>
                //{
                //    Thread.Sleep(1000);

                //    var model = new ProductModel()
                //    {
                //        Code = $"T{index}",
                //        Name = $"Towar {index}",
                //        Ean = $"12345{index}",
                //        Price = index * 0.1M,
                //        Count = index
                //    };

                //    index++;
                //    return model;
                //});

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);                
            }

            return null;
        }

        public static string BuildUrl(string basePath, Dictionary<string, string> queryParams)
        {
            var uriBuilder = new UriBuilder(basePath)
            {
                Query = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"))
            };

            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}
