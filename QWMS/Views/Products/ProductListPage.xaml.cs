using QWMS.ViewModels.Products;

namespace QWMS.Views.Products;

public partial class ProductListPage : ContentPage
{
    ProductListViewModel _viewModel;

    public ProductListPage(ProductListViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.Initialize();

        nie dziala animacja
        Task.Run(() =>
        {
            _viewModel.GetInitialProductsCommand.Execute(false);
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _viewModel.Uninitialize();
    }
}
