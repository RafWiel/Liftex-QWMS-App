using CommunityToolkit.Maui;
using MetroLog.MicrosoftExtensions;
using QWMS.Services;
using QWMS.ViewModels.Dialogs;
using QWMS.Views;
using QWMS.Views.Orders;
using QWMS.ViewModels.Orders;
using QWMS.Views.Dialogs;
using QWMS.Views.Products;
using QWMS.ViewModels.Products;
using QWMS.Interfaces;
using Plugin.Maui.Audio;
using QWMS.Views.Barcodes;
using QWMS.ViewModels.Barcodes;
using QWMS.Views.Reservations;
using QWMS.ViewModels.Reservations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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

            builder.Logging.AddTraceLogger(_ => {});

            builder.Logging.AddConsoleLogger(options =>
            {                
                options.MinLevel = LogLevel.Debug;
                options.MaxLevel = LogLevel.Critical;
            });

            var aa = FileSystem.Current.CacheDirectory;

           builder.Logging.AddStreamingFileLogger(options => 
            {                
                options.MinLevel = LogLevel.Debug;
                options.MaxLevel = LogLevel.Critical;
                options.FolderPath = Path.Combine(
                    FileSystem.Current.CacheDirectory,
                    "QWMS Logs");                
            });

            var fileName = "QWMS.appSettings.production.json";

            #if DEBUG
            fileName = "QWMS.appSettings.json";
            #endif

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName))
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream!)
                    .Build();

                builder.Configuration.AddConfiguration(config);
            }

            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddSingleton<IAudioService, AudioService>();
            builder.Services.AddSingleton<IBarcodeReaderService, BarcodeReaderService>();
            builder.Services.AddSingleton<IMessageDialogsService, MessageDialogsService>();
            builder.Services.AddSingleton<IBarcodesService, BarcodesService>();
            builder.Services.AddSingleton<IOrdersService, OrdersService>();
            builder.Services.AddSingleton<IProductsService, ProductsService>();
            builder.Services.AddSingleton<IReservationsService, ReservationsService>();
            builder.Services.AddScoped<MainPage>();
            builder.Services.AddScoped<BarcodeListPage>();
            builder.Services.AddScoped<BarcodeListViewModel>();
            builder.Services.AddScoped<OrderListPage>();
            builder.Services.AddScoped<OrderListViewModel>();
            builder.Services.AddScoped<OrderDetailsPage>();
            builder.Services.AddScoped<OrderDetailsViewModel>();
            builder.Services.AddScoped<ProductListPage>();
            builder.Services.AddScoped<ProductListViewModel>();
            builder.Services.AddScoped<ProductDetailsPage>();
            builder.Services.AddScoped<ProductDetailsViewModel>();
            builder.Services.AddScoped<ReservationListPage>();
            builder.Services.AddScoped<ReservationListViewModel>();
            builder.Services.AddTransientPopup<MessageDialog, MessageDialogViewModel>();
            builder.Services.AddTransientPopup<AutoMessageDialog, AutoMessageDialogViewModel>();
            builder.Services.AddTransientPopup<ActionMessageDialog, ActionMessageDialogViewModel>();
            builder.Services.AddScoped<QWMS.Interfaces.IConfiguration, Configuration.Configuration>();

            return builder.Build();
        }
    }
}
