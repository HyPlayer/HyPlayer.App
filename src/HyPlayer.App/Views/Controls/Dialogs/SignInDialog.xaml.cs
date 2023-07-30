using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.App.Interfaces.Views;
using HyPlayer.App.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.App.Views.Controls.Dialogs
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
            ViewModel = HyPlayer.App.App.GetDIContainer().ResolveInScope<AccountViewModel>(_scope);
            DataContext = ViewModel;
        }

        private async Task ContentDialog_PrimaryButtonClickAsync(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var result = await ViewModel.SignInAsync(TextBoxAccount.Text, TextBoxPassword.Password);
            HyPlayer.App.App.GetService<INavigationService>().NavigateTo(typeof(Pages.HomePage));
        }
    }
}