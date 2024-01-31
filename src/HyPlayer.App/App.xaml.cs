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
using HyPlayer.Services;
using HyPlayer.PlayCore;
using HyPlayer.PlayCore.Abstraction;
using HyPlayer.ViewModels;

using XamlWindow = Microsoft.UI.Xaml.Window;
using DispatcherQueue = Windows.System.DispatcherQueue;
using Pages = HyPlayer.Views.Pages;
using Window = HyPlayer.Views.Window;
using HyPlayer.Helpers;
using HyPlayer.Interfaces.Services;



namespace HyPlayer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static Frame? contentFrame;

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
            
            DispatcherQueue = DispatcherQueue.GetForCurrentThread();
            Depository = DepositoryFactory.CreateNew();
            
            Depository?.AddMvvm();
            Depository?.AddSingleton<INavigationService, NavigationService>();
            Depository?.AddSingleton<IActivationService, ActivationService>();
            Depository?.AddSingleton<IShellService, ShellService>();
            Depository?.AddSingleton<IPageService, PageService>();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            await GetService<IActivationService>().OnLaunchedAsync(args);
        }      
    }
}
