using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using HyPlayer.Interfaces.Services;
using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using HyPlayer.Views.Pages;

namespace HyPlayer.Views.Controls.Search
{
    public sealed partial class SearchBox : SearchBoxBase
    {
        public SearchBox()
        {
            this.InitializeComponent();
        }

        private void AppSearchBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                HyPlayer.App.GetService<INavigationService>().NavigateTo(typeof(SearchPage), AppSearchBox.Text);
            }
        }
    }

    public class SearchBoxBase : ReactiveControlBase<SearchViewModel>
    {
        
    }
}
