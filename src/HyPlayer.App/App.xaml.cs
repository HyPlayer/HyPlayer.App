using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.Extensions.DependencyInjectionExtensions;
using HyPlayer.Extensions.Helpers;
using HyPlayer.Interfaces.Services;
using HyPlayer.PlayCore;
using HyPlayer.PlayCore.Abstraction;
using HyPlayer.Services;
using HyPlayer.Views.Pages;
using HyPlayer.Views.Window;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using DispatcherQueue = Windows.System.DispatcherQueue;
using Pages = HyPlayer.Views.Pages;
using XamlWindow = Microsoft.UI.Xaml.Window;


namespace HyPlayer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static NavigationView NavView;
        public static Frame? contentFrame;
        private XamlWindow? window;

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

        public static IDepository GetDIContainer()
        {
            return (Current as App)!.Depository!;
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>

        public App()
        {
            this.InitializeComponent();
            
            DispatcherQueue = DispatcherQueue.GetForCurrentThread();
            Depository = DepositoryFactory.CreateNew();
            Depository?.AddMvvm();
            Depository?.AddSingleton<INavigationService, NavigationService>();
            Depository?.AddSingleton<IPageService, PageService>();
            Depository?.AddSingleton<IAppNotificationService, AppNotificationService>();
            Depository?.AddSingleton<INavigationViewService, NavigationViewService>();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            await ConfigurePlayCore();
            window = MainWindow.Current;
            TitleBarHelper.InitializeTitleBarForWindow(window);
            if (window != null)
            {
                NavigateToRootPage(args);

                window?.Activate();
            }
        }

        private async Task ConfigurePlayCore()
        {
            Depository?.AddSingleton<PlayCoreBase, Chopin>();
            var playCore = Depository?.Resolve<PlayCoreBase>();
            if (playCore != null)
            {
                await playCore.RegisterMusicProviderAsync(typeof(NeteaseProvider.NeteaseProvider));
            }
        }

        private void NavigateToRootPage(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            GetService<INavigationService>().NavigateTo("HomePage", args);
        }

    }
}
