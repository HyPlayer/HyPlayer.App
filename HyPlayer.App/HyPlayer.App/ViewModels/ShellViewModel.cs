using CommunityToolkit.Mvvm.ComponentModel;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.App.Interfaces.Views;

namespace HyPlayer.App.ViewModels
{
    public class ShellViewModel : ObservableObject, IScopedViewModel
    {
        public ShellViewModel() 
        { 

        }


        public void GoBack()
        {
            var canGoBack = App.GetService<INavigationService>().CanGoBack;
            if(canGoBack)
            {
                App.GetService<INavigationService>().GoBack();
            }
        }

        public bool CanBack => App.GetService<INavigationService>().CanGoBack;
    }
}
