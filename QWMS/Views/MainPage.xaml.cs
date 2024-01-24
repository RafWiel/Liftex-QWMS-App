using QWMS.Models;
using QWMS.Services;
using QWMS.ViewModels;
using System.Diagnostics;

namespace QWMS.Views
{
    public partial class MainPage : ContentPage
    {
        OrderListViewModel _viewModel;               

        public MainPage(OrderListViewModel viewModel)
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
                _viewModel.GetOrdersCommand.Execute(this);
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _viewModel.Uninitialize();                     
        }                
    }

}

