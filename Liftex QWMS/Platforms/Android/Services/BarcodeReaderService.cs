using Android.Content;
using Com.Cipherlab.Barcode;
using Com.Cipherlab.Barcode.Decoder;
using Com.Cipherlab.Barcode.Decoderparams;
//using Liftex_QWMS.Tools;


namespace Liftex_QWMS.Services
{
    public partial class BarcodeReaderService
    {
        public partial int GetOrientation()
        {
            return 1;
        }

        public delegate void BarcodeReceivedDelegate(string barcode);
        public event BarcodeReceivedDelegate? BarcodeReceived;

        public delegate void DecodeErrorOccurredDelegate(int errorCode);
        public event DecodeErrorOccurredDelegate? DecodeErrorOccurred;

        private Context _context = Android.App.Application.Context;
        private IntentFilter _intentfilter;
        private ReaderManager _readerManager;
        private ReaderDataReceiver _dataReceiver;

        public BarcodeReaderService()
        {
            Console.WriteLine("Android constructor");

            _readerManager = ReaderManager.InitInstance(_context);

            _intentfilter = new IntentFilter();
            _intentfilter.AddAction(GeneralString.IntentREADERSERVICECONNECTED);
            _intentfilter.AddAction(GeneralString.IntentPASSTOAPP);
            _intentfilter.AddAction(GeneralString.IntentDECODEERROR);

            _dataReceiver = new ReaderDataReceiver
            {
                ReaderManager = _readerManager
            };

            _dataReceiver.BarcodeReceived += delegate (string barcode)
            {
                BarcodeReceived?.Invoke(barcode);
            };

            _dataReceiver.DecodeErrorOccurred += delegate (int errorCode)
            {
                DecodeErrorOccurred?.Invoke(errorCode);
            };

            _context.RegisterReceiver(_dataReceiver, _intentfilter);
        }

        public void Dispose()
        {
            Console.WriteLine("Android dispose");

            _context.UnregisterReceiver(_dataReceiver);
            _readerManager.Release();
        }
    }

    public class ReaderDataReceiver : BroadcastReceiver
    {
        public delegate void BarcodeReceivedDelegate(string barcode);
        public event BarcodeReceivedDelegate? BarcodeReceived;

        public delegate void DecodeErrorOccurredDelegate(int errorCode);
        public event DecodeErrorOccurredDelegate? DecodeErrorOccurred;

        public ReaderManager ReaderManager { get; set; }

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals(GeneralString.IntentREADERSERVICECONNECTED))
            {
                ProcessReaderServiceConnected();
                return;
            }

            if (intent.Action.Equals(GeneralString.IntentPASSTOAPP))
            {
                BarcodeReceived?.Invoke(intent.GetStringExtra(GeneralString.BcReaderData));
                return;
            }

            if (intent.Action.Equals(GeneralString.IntentDECODEERROR))
            {
                DecodeErrorOccurred?.Invoke(intent.GetIntExtra(GeneralString.BcReaderDecodeError, 0));
                return;
            }
        }

        private void ProcessReaderServiceConnected()
        {
            try
            {
                //var readerType = ReaderManager.ReaderType;
                //_barcodeTextView.Text = "Reader type " + readerType.ToString();

                var settings = new ReaderOutputConfiguration();
                ReaderManager.Get_ReaderOutputConfiguration(settings);

                settings.EnableKeyboardEmulation = KeyboardEmulationType.None;
                settings.AutoEnterWay = OutputEnterWay.Disable;
                settings.AutoEnterChar = OutputEnterChar.None;
                settings.ShowCodeLen = Enable_State.False;
                settings.ShowCodeType = Enable_State.False;
                settings.SzPrefixCode = "<EAN>";
                settings.SzSuffixCode = "</EAN>";

                ReaderManager.Set_ReaderOutputConfiguration(settings);
                ReaderManager.SetActive(true);

                //Log.Write("Service connected");
            }
            catch (Exception ex)
            {
                //Log.Write(ex.ToString());
            }
        }
    }
}
