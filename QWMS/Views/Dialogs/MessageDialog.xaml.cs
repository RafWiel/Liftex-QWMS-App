using CommunityToolkit.Maui.Views;
using QWMS.ViewModels.Dialogs;

namespace QWMS.Views.Dialogs;

public partial class MessageDialog : Popup
{
    private MessageDialogViewModel _viewModel;

    public MessageDialog(MessageDialogViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;

        _viewModel.CloseEvent += delegate { Close(); };
    }    
}