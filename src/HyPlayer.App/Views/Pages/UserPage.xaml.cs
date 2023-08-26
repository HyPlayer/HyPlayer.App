using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyPlayer.Views.Pages
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
