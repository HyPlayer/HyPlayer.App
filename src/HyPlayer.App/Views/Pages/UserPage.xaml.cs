using HyPlayer.Interfaces.Views;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.ViewModels;
using Microsoft.UI.Xaml.Navigation;


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

            ViewModel.NcmUser = (NeteaseUser)e.Parameter;
            if (ViewModel.NcmUser != null)
            {

                ViewModel.GetUserInfo();
            }
        }
    }

    public class UserPageBase : AppPageWithScopedViewModelBase<UserViewModel>
    {

    }
}
