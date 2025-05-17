using Com.Cipherlab.Barcode.Decoderparams;
using Microsoft.Extensions.Logging;
using QWMS.Helpers;
using QWMS.Interfaces;
using QWMS.Models.Orders;
using QWMS.Models.Barcodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using QWMS.Models;

namespace QWMS.Services
{
    public class BarcodesService : IBarcodesService
    {        
        private HttpClient _httpClient;
        private ILogger<BarcodesService> _logger;
        private IConfiguration _configuration;

        public BarcodesService(ILogger<BarcodesService> logger, IConfiguration configuration) 
        { 
            _logger = logger;
            _configuration = configuration;

            _httpClient = new HttpClient();            
        }

        public async Task<List<BarcodeListModel>?> Get(int productId, int? page)
        {
            try
            {
                var query = new Dictionary<string, string?>
                {
                    { "product-id", productId.ToString() },
                    { "page", page?.ToString() },
                };

                var response = await _httpClient.GetAsync(Tools.BuildUrl($"{_configuration.ApiUrl}/v1/barcodes", query));
                if (!response.IsSuccessStatusCode)
                    return null;

                var items = await response.Content.ReadFromJsonAsync<List<BarcodeListModel>>();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }        
    }
}
