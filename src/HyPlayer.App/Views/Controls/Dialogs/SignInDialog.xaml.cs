using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;


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

        private async Task ContentDialog_PrimaryButtonClickAsync(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            await ViewModel.SignInAsync(TextBoxAccount.Text, TextBoxPassword.Password);
            HyPlayer.App.GetService<INavigationService>().NavigateTo(typeof(Pages.HomePage));
        }
    }
}
