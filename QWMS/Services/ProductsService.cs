using Com.Cipherlab.Barcode.Decoderparams;
using MetroLog;
using Microsoft.Extensions.Logging;
using QWMS.Helpers;
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

        public async Task<List<ProductListModel>?> Get()
        {
            try
            {                
                var response = await _httpClient.GetAsync("http://192.168.1.110:3001/api/v1/products");
                if (!response.IsSuccessStatusCode)
                    return null;

                var models = await response.Content.ReadFromJsonAsync<List<ProductListModel>>();

                return models;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<ProductDetailsModel?> GetSingle(string ean)
        {            
            try
            {
                var query = new Dictionary<string, string>
                {
                    { "ean", ean }
                };
               
                var response = await _httpClient.GetAsync(Tools.BuildUrl("http://192.168.1.110:3001/api/v1/product", query));
                if (!response.IsSuccessStatusCode)
                    return null;

                var model = await response.Content.ReadFromJsonAsync<ProductDetailsModel>();                

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);                
            }

            return null;
        }

        

        
    }
}
