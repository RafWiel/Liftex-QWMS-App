using QWMS.ViewModels.Orders;

namespace QWMS.Views.Orders;

public partial class OrderDetailsPage : ContentPage
{
    OrderDetailsViewModel _viewModel;

    public OrderDetailsPage(OrderDetailsViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.ShowMessage();
    }
}