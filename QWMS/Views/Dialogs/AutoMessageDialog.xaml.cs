using CommunityToolkit.Maui.Views;
using QWMS.ViewModels.Dialogs;

namespace QWMS.Views.Dialogs;

public partial class AutoMessageDialog : Popup
{
    private AutoMessageDialogViewModel _viewModel;

    public AutoMessageDialog(AutoMessageDialogViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;

        _viewModel.CloseEvent += delegate { Close(); };

        this.Opened += delegate { _viewModel.Start(); };
        this.Closed += delegate { _viewModel.Stop(); };
    }    

    
}