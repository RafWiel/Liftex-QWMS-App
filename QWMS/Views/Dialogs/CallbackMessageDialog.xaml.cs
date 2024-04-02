using CommunityToolkit.Maui.Views;
using QWMS.ViewModels.Dialogs;

namespace QWMS.Views.Dialogs;

public partial class CallbackMessageDialog : Popup
{
    private CallbackMessageDialogViewModel _viewModel;

    public CallbackMessageDialog(CallbackMessageDialogViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;

        _viewModel.CloseEvent += delegate { Close(); };

        this.Opened += delegate
        {
            _viewModel.PlayInitialSound();          
        };
    }    
}