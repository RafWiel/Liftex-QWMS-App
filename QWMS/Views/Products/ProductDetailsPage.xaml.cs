using QWMS.ViewModels.Products;

namespace QWMS.Views.Products;

public partial class ProductDetailsPage : ContentPage
{
    private ProductDetailsViewModel _viewModel;

    public ProductDetailsPage(ProductDetailsViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }
}