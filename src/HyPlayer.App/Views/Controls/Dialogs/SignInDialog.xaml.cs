using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using Microsoft.UI.Xaml.Controls;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.Views.Controls.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInDialog : ContentDialog
    {
        private readonly IDepositoryResolveScope _scope;
        private AccountViewModel ViewModel;

        public SignInDialog()
        {
            this.InitializeComponent();

            _scope = DepositoryResolveScope.Create();
            ViewModel = HyPlayer.App.GetDIContainer().ResolveInScope<AccountViewModel>(_scope);
            DataContext = ViewModel;
        }


        private void CancelButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Hide();
        }

        private void LoginButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Login();
        }
        public async void Login()
        {
            await ViewModel.SignInAsync(TextBoxAccount.Text, TextBoxPassword.Password);
            if (!ViewModel.IsLogin)
            {
                LoginFailedInfoBar.IsOpen = true;
                return;
            }
            HyPlayer.App.GetService<INavigationService>().NavigateTo(typeof(Pages.HomePage));
            Hide();
        }

        private void ContentDialog_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (TextBoxAccount.Text == "")
            {
                TextBoxAccount.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
                return;
            }
            if (TextBoxPassword.Password == "")
            {
                TextBoxPassword.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
                return;
            }
            Login();
        }
    }
}
