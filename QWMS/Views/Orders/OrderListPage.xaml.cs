using QWMS.ViewModels.Orders;

namespace QWMS.Views.Orders;

public partial class OrderListPage : ContentPage
{
    OrderListViewModel _viewModel;

    public OrderListPage(OrderListViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.Initialize();

        Task.Run(() =>
        {
            _viewModel.GetOrdersCommand.Execute(false);
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _viewModel.Uninitialize();
    }
}
