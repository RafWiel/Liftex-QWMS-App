using Com.Cipherlab.Barcode.Decoderparams;
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
        private IConfiguration _configuration;

        public ProductsService(ILogger<ProductsService> logger, IConfiguration configuration) 
        { 
            _logger = logger;
            _configuration = configuration;

            _httpClient = new HttpClient();            
        }

        public async Task<List<ProductListModel>?> Get(string? search, int? page)
        {
            try
            {
                var query = new Dictionary<string, string?>
                {
                    { "search", search },
                    { "page", page?.ToString() },
                };

                _logger.LogInformation("Pobieranie listy towarów");

                var response = await _httpClient.GetAsync(Tools.BuildUrl($"{_configuration.ApiUrl}/v1/products", query));
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

        public async Task<ProductDetailsModel?> GetSingle(int id)
        {
            var query = new Dictionary<string, string?>
            {
                { "id", id.ToString() }
            };

            _logger.LogInformation($"Pobieranie towaru id {id}");

            return await GetSingle(query);
        }

        public async Task<ProductDetailsModel?> GetSingle(string ean)
        {
            var query = new Dictionary<string, string?>
            {
                { "ean", ean }
            };

            _logger.LogInformation($"Pobieranie towaru ean {ean}");

            return await GetSingle(query);
        }

        private async Task<ProductDetailsModel?> GetSingle(Dictionary<string, string?> query)
        {
            try
            {                
                var response = await _httpClient.GetAsync(Tools.BuildUrl($"{_configuration.ApiUrl}/v1/product", query));
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
