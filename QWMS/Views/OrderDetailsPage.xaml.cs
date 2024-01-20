using QWMS.ViewModels;

namespace QWMS.Views;

public partial class OrderDetailsPage : ContentPage
{
	public OrderDetailsPage(OrderDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}