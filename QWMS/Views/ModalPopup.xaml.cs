using CommunityToolkit.Maui.Views;
using QWMS.ViewModels;

namespace QWMS.Views;

public partial class ModalPopup : Popup
{
    private ModalPopupViewModel _viewModel;

    public ModalPopup(ModalPopupViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel;
        BindingContext = viewModel;
    }
}