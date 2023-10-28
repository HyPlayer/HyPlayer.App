namespace HyPlayer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static SystemInformation systemInfo => SystemInformation.Instance;
        public static CommunityToolkit.WinUI.Helpers.ThemeListener themeListener = new();
        public static Frame? contentFrame;
        private Window? window;

        public IDepository? Depository;
        public DispatcherQueue? DispatcherQueue;

        public static T GetService<T>()
            where T : class
        {
            if ((App.Current as App)!.Depository?.ResolveDependency(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }

            return service;
        }

        public static DispatcherQueue? GetDispatcherQueue()
        {
            return (Current as App)!.DispatcherQueue;
        }

        public static IDepository? GetDIContainer()
        {
            return (Current as App)!.Depository;
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>

        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            
            DispatcherQueue = DispatcherQueue.GetForCurrentThread();
            Depository = DepositoryFactory.CreateNew();
            ConfigureServices();
            await ConfigurePlayCore();

            GetService<IActivationService>().OnActivated();
        }
        
        private void ConfigureServices()
        {
            Depository?.AddMvvm();
            Depository?.AddSingleton<INavigationService, NavigationService>();
            Depository?.AddSingleton<IWindowManagementService, WindowManagementService>();
            Depository?.AddSingleton<IActivationService, ActivationService>();
        }

        private async Task ConfigurePlayCore()
        {
            Depository?.AddSingleton<PlayCoreBase, Chopin>();
            var playCore = Depository?.Resolve<PlayCoreBase>();
            await playCore.RegisterMusicProviderAsync(typeof(NeteaseProvider.NeteaseProvider));
        }      
    }
}
