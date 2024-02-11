using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Interfaces
{
    public interface IBarcodeReaderService
    {
        delegate void BarcodeReceivedDelegate(string barcode);
        event BarcodeReceivedDelegate? BarcodeReceived;

        delegate void DecodeErrorOccurredDelegate(int errorCode);
        event DecodeErrorOccurredDelegate? DecodeErrorOccurred;

        void Initialize();
        void Dispose();
    }
}
