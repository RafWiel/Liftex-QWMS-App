using QWMS.Views.Orders;
using QWMS.Views.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS
{
    public partial class AppShell : Shell
    {
        private void ConfigureRouting()
        {
            Routing.RegisterRoute(nameof(OrderListPage), typeof(OrderListPage));
            Routing.RegisterRoute(nameof(OrderDetailsPage), typeof(OrderDetailsPage));
            Routing.RegisterRoute(nameof(ProductListPage), typeof(ProductListPage));
            Routing.RegisterRoute(nameof(ProductDetailsPage), typeof(ProductDetailsPage));
        }
    }
}
