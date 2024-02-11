using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Services
{
    public partial class BarcodeReaderService : IBarcodeReaderService, IDisposable
    {
        private ILogger<BarcodeReaderService> _logger;

        public event IBarcodeReaderService.BarcodeReceivedDelegate? BarcodeReceived;
        public event IBarcodeReaderService.DecodeErrorOccurredDelegate? DecodeErrorOccurred;

        public BarcodeReaderService(ILogger<BarcodeReaderService> logger)
        {
            _logger = logger;

            Initialize();
        }

        public partial void Initialize();
        public partial void Dispose();
    }
}
