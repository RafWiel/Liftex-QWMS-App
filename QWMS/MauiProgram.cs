using Microsoft.Extensions.Logging;
using QWMS.Services;
using QWMS.ViewModels;
using QWMS.Views;

namespace QWMS
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<OrderListService>();
            builder.Services.AddTransient<MainPage>();            
            builder.Services.AddTransient<OrderListViewModel>();
            builder.Services.AddTransient<OrderDetailsPage>();
            builder.Services.AddTransient<OrderDetailsViewModel>();

            return builder.Build();
        }
    }
}
