using Android.Content;
using Android.Text;
using AndroidX.Lifecycle;
using Com.Cipherlab.Barcode;
using Com.Cipherlab.Barcode.Decoder;
using Com.Cipherlab.Barcode.Decoderparams;
using Java.Util.Logging;
using MetroLog;
using Microsoft.Extensions.Logging;
using QWMS.Interfaces;
using System.Text.RegularExpressions;
//using QWMS.Tools;


namespace QWMS.Services
{
    public partial class BarcodeReaderService : IBarcodeReaderService, IDisposable
    {                
        private Context _context = Android.App.Application.Context;
        private IntentFilter _intentfilter = new IntentFilter();
        private ReaderManager? _readerManager;
        private ReaderDataReceiver? _dataReceiver;

        public partial void Initialize()
        {
            Console.WriteLine("Android constructor");

            _readerManager = ReaderManager.InitInstance(_context);
            
            _intentfilter.AddAction(GeneralString.IntentREADERSERVICECONNECTED);
            _intentfilter.AddAction(GeneralString.IntentPASSTOAPP);
            _intentfilter.AddAction(GeneralString.IntentDECODEERROR);

            _dataReceiver = new ReaderDataReceiver
            {
                ReaderManager = _readerManager,
                Logger = _logger,
            };

            _dataReceiver.BarcodeReceived += delegate (string barcode)
            {
                BarcodeReceived?.Invoke(barcode);
            };

            _dataReceiver.DecodeErrorOccurred += delegate (int errorCode)
            {
                _logger.LogError($"Barcode reader error occurred: {errorCode}");
                DecodeErrorOccurred?.Invoke(errorCode);                
            };

            _context.RegisterReceiver(_dataReceiver, _intentfilter);
        }

        public partial void Dispose()
        {
            Console.WriteLine("Android dispose");

            _context.UnregisterReceiver(_dataReceiver);
            _readerManager?.Release();
        }
    }

    public class ReaderDataReceiver : BroadcastReceiver
    {
        public delegate void BarcodeReceivedDelegate(string barcode);
        public event BarcodeReceivedDelegate? BarcodeReceived;

        public delegate void DecodeErrorOccurredDelegate(int errorCode);
        public event DecodeErrorOccurredDelegate? DecodeErrorOccurred;

        public ReaderManager? ReaderManager { get; set; }
        public ILogger<BarcodeReaderService>? Logger { get; set; }

        public override void OnReceive(Context? context, Intent? intent)
        {
            if (intent == null) 
                return;

            if (intent.Action == null)
                return;

            if (intent.Action.Equals(GeneralString.IntentREADERSERVICECONNECTED))
            {
                ProcessReaderServiceConnected();
                return;
            }

            if (intent.Action.Equals(GeneralString.IntentPASSTOAPP))
            {
                ProcessBarcode(intent.GetStringExtra(GeneralString.BcReaderData));                
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
                ReaderManager?.Get_ReaderOutputConfiguration(settings);

                settings.EnableKeyboardEmulation = KeyboardEmulationType.None;
                settings.AutoEnterWay = OutputEnterWay.Disable;
                settings.AutoEnterChar = OutputEnterChar.None;
                settings.ShowCodeLen = Enable_State.False;
                settings.ShowCodeType = Enable_State.False;
                settings.SzPrefixCode = "<ean>";
                settings.SzSuffixCode = "</ean>";

                ReaderManager?.Set_ReaderOutputConfiguration(settings);
                ReaderManager?.SetActive(true);

                //Log.Write("Service connected");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, ex.Message);
            }
        }

        private void ProcessBarcode(string? barcode)
        {
            if (barcode == null)
                return;

            Match m = Regex.Match(barcode, "^<ean>\\w+</ean>$");
            if (m.Success == false)
                return;
            
            BarcodeReceived?.Invoke(Regex.Replace(barcode, "(?:^<ean>|</ean>$)", string.Empty));
        }
    }
}
