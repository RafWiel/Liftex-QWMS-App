using QWMS.Views;

namespace QWMS
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(OrderDetailsPage), typeof(OrderDetailsPage));
        }
    }
}
