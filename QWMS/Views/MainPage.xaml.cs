using QWMS.Models;
using QWMS.Services;
using QWMS.ViewModels;
using System.Diagnostics;

namespace QWMS.Views
{
    public partial class MainPage : ContentPage
    {
        OrderListViewModel _viewModel;
        //private BarcodeReaderService _barcodeReader;

        int count = 0;

        public MainPage(OrderListViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            BindingContext = viewModel;

            //_barcodeReader = new BarcodeReaderService();            

            //#if ANDROID
            //_barcodeReader.BarcodeReceived += _barcodeReader_BarcodeReceived;  
            //#endif
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(() =>
            {
                _viewModel.GetOrdersCommand.Execute(this);
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //_barcodeReader.Dispose();
        }

        private void _barcodeReader_BarcodeReceived(string barcode)
        {
            //CounterBtn.Text = barcode;
        }

        //private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        //{
        //    var order = ((VisualElement)sender).BindingContext as OrderModel;
        //    if (order == null)
        //        return;

        //    _viewModel.GoToDetailsCommand.Execute(order);

        //    Debug.WriteLine($"tapped order {order.Name}");
        //}
    }

}

