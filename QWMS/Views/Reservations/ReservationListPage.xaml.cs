using QWMS.ViewModels.Reservations;

namespace QWMS.Views.Reservations;

public partial class ReservationListPage : ContentPage
{
    ReservationListViewModel _viewModel;

    public ReservationListPage(ReservationListViewModel viewModel)
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
