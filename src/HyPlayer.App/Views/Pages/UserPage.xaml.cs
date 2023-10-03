using Microsoft.UI.Xaml.Navigation;
using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using HyPlayer.NeteaseProvider.Models;


namespace HyPlayer.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserPage : UserPageBase
    {
        public UserPage()
        {
            this.InitializeComponent();   
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.NcmUser = (NeteaseUser) e.Parameter;
            if(ViewModel.NcmUser != null)
            {
                
                ViewModel.GetUserInfo();
            }
        }
    }

    public class UserPageBase : AppPageWithScopedViewModelBase<UserViewModel>
    {

    }
}
