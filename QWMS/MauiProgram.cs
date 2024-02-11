﻿using CommunityToolkit.Maui;
using MetroLog.MicrosoftExtensions;
using QWMS.Services;
using QWMS.ViewModels.Dialogs;
using QWMS.Views;
using QWMS.Views.Orders;
using QWMS.Views.Dialogs;
using QWMS.ViewModels.Orders;
using QWMS.Views.Products;
using QWMS.ViewModels.Products;
using QWMS.Interfaces;
using Plugin.Maui.Audio;

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

            builder.Logging.AddTraceLogger(_ => {}); //TODO: Ustaw plik do zapisu

            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddSingleton<IAudioService, AudioService>();
            builder.Services.AddSingleton<IBarcodeReaderService, BarcodeReaderService>();
            builder.Services.AddSingleton<IMessageDialogsService, MessageDialogsService>();
            builder.Services.AddSingleton<IOrdersService, OrdersService>();
            builder.Services.AddSingleton<IProductsService, ProductsService>();
            builder.Services.AddScoped<MainPage>();
            builder.Services.AddScoped<OrderListPage>();
            builder.Services.AddScoped<OrderListViewModel>();
            builder.Services.AddScoped<OrderDetailsPage>();
            builder.Services.AddScoped<OrderDetailsViewModel>();
            builder.Services.AddScoped<ProductDetailsPage>();
            builder.Services.AddScoped<ProductDetailsViewModel>();
            builder.Services.AddTransientPopup<MessageDialog, MessageDialogViewModel>();
            builder.Services.AddTransientPopup<AutoMessageDialog, AutoMessageDialogViewModel>();

            return builder.Build();
        }
    }
}
