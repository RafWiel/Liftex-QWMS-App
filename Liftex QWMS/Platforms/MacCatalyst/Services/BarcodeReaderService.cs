//using Liftex_QWMS.Tools;


namespace Liftex_QWMS.Services
{
    public partial class BarcodeReaderService
    {
        public partial int GetOrientation()
        {
            return 2;
        }

        //public delegate void BarcodeReceivedDelegate(string barcode);
        //public event BarcodeReceivedDelegate? BarcodeReceived;

        //public delegate void DecodeErrorOccurredDelegate(int errorCode);
        //public event DecodeErrorOccurredDelegate? DecodeErrorOccurred;

        //private Context _context = Android.App.Application.Context;
        //private IntentFilter _intentfilter;
        //private ReaderManager _readerManager;
        //private ReaderDataReceiver _dataReceiver;

        //public BarcodeReaderService()
        //{
        //    Console.WriteLine("Android constructor");

        //    _readerManager = ReaderManager.InitInstance(_context);

        //    _intentfilter = new IntentFilter();
        //    _intentfilter.AddAction(GeneralString.IntentREADERSERVICECONNECTED);
        //    _intentfilter.AddAction(GeneralString.IntentPASSTOAPP);
        //    _intentfilter.AddAction(GeneralString.IntentDECODEERROR);

        //    _dataReceiver = new ReaderDataReceiver
        //    {
        //        ReaderManager = _readerManager
        //    };

        //    _dataReceiver.BarcodeReceived += delegate (string barcode)
        //    {
        //        BarcodeReceived?.Invoke(barcode);
        //    };

        //    _dataReceiver.DecodeErrorOccurred += delegate (int errorCode)
        //    {
        //        DecodeErrorOccurred?.Invoke(errorCode);
        //    };

        //    _context.RegisterReceiver(_dataReceiver, _intentfilter);
        //}       

        //public void Dispose()
        //{
        //    Console.WriteLine("Android dispose");

        //    _context.UnregisterReceiver(_dataReceiver);
        //    _readerManager.Release();
        //}
    }
}
