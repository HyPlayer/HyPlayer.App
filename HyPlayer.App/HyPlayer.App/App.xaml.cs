using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WinUIEx;
using System;

using HyPlayer.NeteaseApi;
using HyPlayer.App.Contract.Services;
using HyPlayer.App.Services;
using Window = HyPlayer.App.Views.Window;
using Pages = HyPlayer.App.Views.Pages;
using ViewModels.App;
using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseProvider;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static Frame contentFrame;
        public static WindowEx window;
        public static NeteaseProvider.NeteaseProvider provider;

        public IHost Host;

        public static T GetService<T>()
            where T : class
        {
            if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }

            return service;
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
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {

            Host = Microsoft.Extensions.Hosting.Host.
                CreateDefaultBuilder().
                UseContentRoot(AppContext.BaseDirectory).
                ConfigureServices((context, services) =>
                {
                    // services.AddSingleton<IInitializeService, InitializeService>();
                    services.AddSingleton<INavigationService, NavigationService>();
                    services.AddSingleton<IPageService, PageService>();

                    services.AddTransient<HomeViewModel>();
                }).Build();

            // App.GetService<IInitializeService>().StartUpAsync();

            window = new Window.MainWindow();
            window.Activate();
        }

        protected private void NavigationToRootPage(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {   
            App.GetService<INavigationService>().NavigateTo(typeof(Pages.HomePage));
            
        }

        
    }
}
