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
using HyPlayer.App.Interfaces.Views;
using HyPlayer.App.ViewModels;
using AsyncAwaitBestPractices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.App.Views.Controls.Home
{
    public sealed partial class RecommendPlayListSelection : RecommendPlayListSelectionBase
    {
        public RecommendPlayListSelection()
        {
            this.InitializeComponent();
        }

        private void RecommendPlayListSelectionBase_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GetPlayListAsync().SafeFireAndForget();
        }
    }

    public class RecommendPlayListSelectionBase : ReactiveControlBase<HomeViewModel>
    {
        
    }
}
