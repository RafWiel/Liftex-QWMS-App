using Microsoft.Extensions.Logging;

namespace QWMS
{
    public partial class App : Application
    {
        private ILogger<App> _logger;

        public App(ILogger<App> logger)
        {
            _logger = logger;

            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {            
            var window = base.CreateWindow(activationState);

            #if WINDOWS
            window.Width = 400;
            window.Height = 600;
            #endif

            //var display = DeviceDisplay.Current.MainDisplayInfo;

            //// move to screen center
            //window.X = (display.Width / display.Density - window.Width) / 2;
            //window.Y = (display.Height / display.Density - window.Height) / 2;

            _logger.LogInformation("Application start");

            return window;
        }
    }
}
