using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Dispatching;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using WinUIEx;
using HyPlayer.Extensions.DependencyInjectionExtensions;
using HyPlayer.Interfaces.Views;
using HyPlayer.Services;
using HyPlayer.PlayCore;
using HyPlayer.PlayCore.Abstraction;
using HyPlayer.ViewModels;
using HyPlayer.Extensions.Helpers;

using XamlWindow = Microsoft.UI.Xaml.Window;
using DispatcherQueue = Windows.System.DispatcherQueue;
using Pages = HyPlayer.Views.Pages;
using Window = HyPlayer.Views.Window;
using HyPlayer.Views.Window;



namespace HyPlayer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static Frame? contentFrame;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            UnhandledException += App_UnhandledException;
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            await EnsureEarlyApp();
      
            if(MainWindow.Instance != null) 
            { 
                NavigateToRootPage(args);

                MainWindow.Instance?.Activate();
            }
        }

        private void NavigateToRootPage(LaunchActivatedEventArgs args)
        {   
            GetService<INavigationService>().NavigateTo(typeof(Pages.HomePage));
        }

        private async Task EnsureEarlyApp()
        {
            DispatcherQueue = DispatcherQueue.GetForCurrentThread();
            Depository = DepositoryFactory.CreateNew();

            // Configure ViewModels.
            Depository?.AddMvvm();

            // Configure Services.
            Depository?.AddSingleton<INavigationService, NavigationService>();
            Depository?.AddSingleton<IPageService, PageService>();

            // Configure PlayCore.
            Depository?.AddSingleton<PlayCoreBase, Chopin>();
            var playCore = Depository?.Resolve<PlayCoreBase>();
            await playCore.RegisterMusicProviderAsync(typeof(NeteaseProvider.NeteaseProvider));
        }

        #region Exception Handlers        
        private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        #region Dependency Injection Container
        public IDepository? Depository;
        public DispatcherQueue? DispatcherQueue;

        public static T GetService<T>()
            where T : class
        {
            if ((Current as App)!.Depository?.ResolveDependency(typeof(T)) is not T service)
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
        #endregion
    }
}
