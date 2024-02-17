using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUIEx;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.Extensions.DependencyInjectionExtensions;
using HyPlayer.Interfaces.Views;
using HyPlayer.Services;
using HyPlayer.PlayCore;
using HyPlayer.PlayCore.Abstraction;
using Microsoft.UI.Dispatching;
using DispatcherQueue = Windows.System.DispatcherQueue;
using Window = HyPlayer.Views.Window;
using XamlWindow = Microsoft.UI.Xaml.Window;
using Pages = HyPlayer.Views.Pages;
using HyPlayer.ViewModels;
using HyPlayer.Extensions.Helpers;
using HyPlayer.Views.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
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
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            DispatcherQueue = DispatcherQueue.GetForCurrentThread();
            Depository = DepositoryFactory.CreateNew();
            ConfigureServices();
            await ConfigurePlayCore();
            window = WindowHelper.CreateBlankWindow();
            if(window != null) 
            {
                window.Content = new ShellPage();

                window?.Activate();
            }
        }
        
        private void ConfigureServices()
        {
            Depository?.AddMvvm();
            Depository?.AddSingleton<INavigationService, NavigationService>();
            Depository?.AddSingleton<IPageService, PageService>();
        }

        private async Task ConfigurePlayCore()
        {
            Depository?.AddSingleton<PlayCoreBase, Chopin>();
            var playCore = Depository?.Resolve<PlayCoreBase>();
            await playCore.RegisterMusicProviderAsync(typeof(NeteaseProvider.NeteaseProvider));
        }

        private void NavigateToRootPage(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {   
            GetService<INavigationService>().NavigateTo(typeof(Pages.HomePage));
        }
       
    }
}
