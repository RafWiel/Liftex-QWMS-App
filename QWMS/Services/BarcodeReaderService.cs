using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Services
{
    public partial class BarcodeReaderService : IDisposable
    {
        private ILogger<BarcodeReaderService> _logger;

        public BarcodeReaderService(ILogger<BarcodeReaderService> logger)
        {
            _logger = logger;

            Initialize();
        }

        public partial void Initialize();
        public partial void Dispose();
    }
}
