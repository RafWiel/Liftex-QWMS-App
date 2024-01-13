﻿using Liftex_QWMS.Services;
using System.Threading.Tasks;

namespace Liftex_QWMS
{
    public partial class MainPage : ContentPage
    {
        private BarcodeReaderService _barcodeReader;

        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            _barcodeReader = new BarcodeReaderService();

            var x = _barcodeReader.GetOrientation();

#if ANDROID
            _barcodeReader.BarcodeReceived += _barcodeReader_BarcodeReceived;  
#endif
        }

        private void _barcodeReader_BarcodeReceived(string barcode)
        {
            CounterBtn.Text = barcode;            
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
