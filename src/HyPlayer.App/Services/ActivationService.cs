using Depository.Extensions;
using HyPlayer.Interfaces.Services;
using HyPlayer.PlayCore.Abstraction;
using HyPlayer.PlayCore;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyPlayer.Helpers;
using Microsoft.Windows.AppLifecycle;

namespace HyPlayer.Services
{
    class ActivationService : IActivationService
    {
        private UIElement _shell = null;

        public ActivationService()
        {
            
        }

        public async Task OnLaunchedAsync(object ActivateEventArgs)
        {
            AppInstance MainInstance = AppInstance.FindOrRegisterForKey("main");
            if (!MainInstance.IsCurrent)
            {
                var activatedEventArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
                await MainInstance.RedirectActivationToAsync(activatedEventArgs);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return;
            }
            await StartupAsync(); 

            var ActivatedEventArgs = AppInstance.GetCurrent().GetActivatedEventArgs();

            if(ActivatedEventArgs != null) 
            {
                Window MainWindow = WindowHelper.CurrentMainWindow;
                if (ActivatedEventArgs.Kind == ExtendedActivationKind.Launch)
                {
                    App.GetService<INavigationService>().NavigateTo(typeof(Views.Pages.HomePage), ActivateEventArgs);
                }
                MainWindow.Activate();
            }
            
        }

        private async Task StartupAsync()
        {
            (App.Current as App)?.Depository?.AddSingleton<PlayCoreBase, Chopin>();
            var playCore = (App.Current as App)?.Depository?.Resolve<PlayCoreBase>();
            await playCore.RegisterMusicProviderAsync(typeof(NeteaseProvider.NeteaseProvider));
        }
    }
}
