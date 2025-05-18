using Com.Cipherlab.Barcode.Decoderparams;
using Microsoft.Extensions.Logging;
using QWMS.Helpers;
using QWMS.Interfaces;
using QWMS.Models.Orders;
using QWMS.Models.Reservations;
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
    public class ReservationsService : IReservationsService
    {        
        private HttpClient _httpClient;
        private ILogger<ReservationsService> _logger;
        private IConfiguration _configuration;

        public ReservationsService(ILogger<ReservationsService> logger, IConfiguration configuration) 
        { 
            _logger = logger;
            _configuration = configuration;

            _httpClient = new HttpClient();
        }

        public async Task<List<ReservationListModel>?> Get(int productId, int? page)
        {
            try
            {
                var query = new Dictionary<string, string?>
                {
                    { "product-id", productId.ToString() },
                    { "page", page?.ToString() },
                };

                _logger.LogInformation($"Pobieranie listy rezerwacji towaru id {productId}");

                var response = await _httpClient.GetAsync(Tools.BuildUrl($"{_configuration.ApiUrl}/v1/reservations", query));
                if (!response.IsSuccessStatusCode)
                    return null;

                var items = await response.Content.ReadFromJsonAsync<List<ReservationListModel>>();

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
