using HyPlayer.App.Interfaces.Views;
using HyPlayer.App.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyPlayer.App.Views.Pages
{
    public partial class UserPage : Page
    {
        public UserPage()
        {
            this.InitializeComponent();
        }
    }

    public class UserPageBase : AppPageWithScopedViewModelBase<UserViewModel>
    {

    }
}
