namespace HyPlayer.Services
{
    public class ActivationService : IActivationService
    {
        private AppInstance _appInstance => AppInstance.GetCurrent();

        public void OnActivated()
        {
            var activateKind = _appInstance.GetActivatedEventArgs().Kind;
            
            if (activateKind == ExtendedActivationKind.Launch)
            {
            }

            var window = App.GetService<IWindowManagementService>().Current;
            if (window != null)
            {
                App.GetService<IWindowManagementService>().EnsureEarlyWindow(window);


                var rootFrame = new Frame();
                window.Content = rootFrame;

                rootFrame.Navigate(typeof(ShellPage), _appInstance.GetActivatedEventArgs());
                NavigateToRootPage(_appInstance.GetActivatedEventArgs());

                window?.Activate();
            }

            
        }

        private void NavigateToRootPage(object args)
        {
            App.GetService<INavigationService>().NavigateTo(typeof(HomePage));
        }
    }
}
