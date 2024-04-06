using CommunityToolkit.Maui.Views;
using QWMS.ViewModels.Dialogs;

namespace QWMS.Views.Dialogs;

public partial class ActionMessageDialog : Popup
{
    private ActionMessageDialogViewModel _viewModel;

    public ActionMessageDialog(ActionMessageDialogViewModel viewModel)
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