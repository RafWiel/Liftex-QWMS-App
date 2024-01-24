﻿using Microsoft.Extensions.Logging;
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

            builder.Services.AddSingleton<BarcodeReaderService>();
            builder.Services.AddSingleton<OrderListService>();
            builder.Services.AddScoped<MainPage>();            
            builder.Services.AddScoped<OrderListViewModel>();
            builder.Services.AddScoped<OrderDetailsPage>();
            builder.Services.AddScoped<OrderDetailsViewModel>();

            return builder.Build();
        }
    }
}
