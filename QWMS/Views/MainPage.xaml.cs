using QWMS.Services;

namespace QWMS.Views
{
    public partial class MainPage : ContentPage
    {
        private BarcodeReaderService _barcodeReader;

        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            _barcodeReader = new BarcodeReaderService();            

            #if ANDROID
            _barcodeReader.BarcodeReceived += _barcodeReader_BarcodeReceived;  
            #endif
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _barcodeReader.Dispose();
        }

        private void _barcodeReader_BarcodeReceived(string barcode)
        {
            //CounterBtn.Text = barcode;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            //if (count == 1)
            //    CounterBtn.Text = $"Clicked {count} time";
            //else
            //    CounterBtn.Text = $"Clicked {count} times";

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}

https://www.youtube.com/watch?v=XmdBXuNPShs&list=PLfbOp004UaYVt1En4WW3pVuM-vm66OqZe&index=5&t=1180s