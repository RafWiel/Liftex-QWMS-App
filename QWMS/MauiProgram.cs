﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using QWMS.Services;
using QWMS.ViewModels;
using QWMS.ViewModels.Dialogs;
using QWMS.Views;
using QWMS.Views.Dialogs;

namespace QWMS
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp
                .CreateBuilder()
                .UseMauiCommunityToolkit()
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
            builder.Services.AddTransientPopup<MessageDialog, MessageDialogViewModel>();
            builder.Services.AddTransientPopup<AutoMessageDialog, AutoMessageDialogViewModel>();

            return builder.Build();
        }
    }
}
