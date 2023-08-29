using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
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
