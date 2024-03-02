using QWMS.ViewModels.Barcodes;

namespace QWMS.Views.Barcodes;

public partial class BarcodeListPage : ContentPage
{
    BarcodeListViewModel _viewModel;

    public BarcodeListPage(BarcodeListViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.Initialize();                
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _viewModel.Uninitialize();
    }
}
