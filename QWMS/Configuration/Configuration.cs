using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using QWMS.Interfaces;
using QWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Configuration
{    
    public class Configuration : QWMS.Interfaces.IConfiguration
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private ILogger<Configuration> _logger;

        public string ApiUrl { get; private set; } = string.Empty;

        public Configuration(Microsoft.Extensions.Configuration.IConfiguration configuration, ILogger<Configuration> logger)
        {
            _configuration = configuration;
            _logger = logger;

            Initialize();                       
        }

        private void Initialize()
        {            
            try
            {
                var settings = _configuration.GetRequiredSection("Settings").Get<SettingsModel>();
                
                ApiUrl = settings!.ApiUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
