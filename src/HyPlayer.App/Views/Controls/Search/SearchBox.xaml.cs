using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using HyPlayer.App.Interfaces.Views;
using HyPlayer.App.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;


namespace HyPlayer.App.Views.Controls.Search
{
    public sealed partial class SearchBox : SearchBoxBase
    {
        public SearchBox()
        {
            this.InitializeComponent();
        }
    }

    public class SearchBoxBase : ReactiveUserControl<SearchViewModel>
    {
        
    }
}
