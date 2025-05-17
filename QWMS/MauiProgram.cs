using CommunityToolkit.Maui;
//using MetroLog.MicrosoftExtensions;
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
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Serilog;
using System.Diagnostics;
using System.Text;
using Microsoft.Maui.Controls.PlatformConfiguration;

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

            var services = builder.Services;


            #if ANDROID
            var logFilePath = Path.Combine(
                Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath,
                "QWMS Logs",
                "qwms log .txt");            
            #else
            var logFilePath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
                "QWMS Logs",
                "qwms log .txt");
            #endif

            services.AddLogging(options =>
            {
                options.AddSerilog(
                    new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .WriteTo.Debug()                        
                        .WriteTo.File(
                            logFilePath,
                            rollingInterval: (RollingInterval)RollingInterval.Day,
                            retainedFileCountLimit: 30,
                            flushToDiskInterval: TimeSpan.FromSeconds(5),
                            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                        //.WriteTo.Seq("http://localhost:5341/")
                        //.WriteTo.Email(options: new()
                        //{
                        //    From = "MauiApp@test.com",
                        //    To = ["MauiAppSupport@test.com"],
                        //    Host = "smtp4dev.example.com",
                        //    Port = 2525
                        //},
                        //batchingOptions: new()
                        //{
                        //    BatchSizeLimit = 10,
                        //    BufferingTimeLimit = TimeSpan.FromSeconds(30)
                        //})
                        .CreateLogger());
            });
                        
            //Debug.WriteLine("Log file location:>" + logFilePath);
            //if (File.Exists(logFilePath))
            //{
            //    var logContent = File.ReadAllText(logFilePath);
            //    Debug.WriteLine("logContent:>>" + logContent);
            //}
            
            services.AddSingleton(AudioManager.Current);
            services.AddSingleton<IAudioService, AudioService>();
            services.AddSingleton<IBarcodeReaderService, BarcodeReaderService>();
            services.AddSingleton<IMessageDialogsService, MessageDialogsService>();
            services.AddSingleton<IBarcodesService, BarcodesService>();
            services.AddSingleton<IOrdersService, OrdersService>();
            services.AddSingleton<IProductsService, ProductsService>();
            services.AddSingleton<IReservationsService, ReservationsService>();
            services.AddScoped<MainPage>();
            services.AddScoped<BarcodeListPage>();
            services.AddScoped<BarcodeListViewModel>();
            services.AddScoped<OrderListPage>();
            services.AddScoped<OrderListViewModel>();
            services.AddScoped<OrderDetailsPage>();
            services.AddScoped<OrderDetailsViewModel>();
            services.AddScoped<ProductListPage>();
            services.AddScoped<ProductListViewModel>();
            services.AddScoped<ProductDetailsPage>();
            services.AddScoped<ProductDetailsViewModel>();
            services.AddScoped<ReservationListPage>();
            services.AddScoped<ReservationListViewModel>();
            services.AddTransientPopup<MessageDialog, MessageDialogViewModel>();
            services.AddTransientPopup<AutoMessageDialog, AutoMessageDialogViewModel>();
            services.AddTransientPopup<ActionMessageDialog, ActionMessageDialogViewModel>();
            services.AddScoped<QWMS.Interfaces.IConfiguration, Configuration.Configuration>();

            return builder.Build();
        }
    }
}
