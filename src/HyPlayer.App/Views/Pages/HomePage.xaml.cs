using Depository.Abstraction.Interfaces;
using Depository.Core;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Depository.Abstraction.Models.Options;
using Depository.Extensions;
using HomeViewModel = HyPlayer.App.ViewModels.HomeViewModel;
using AsyncAwaitBestPractices;
using HyPlayer.App.Interfaces.Views;
using HyPlayer.PlayCore.Abstraction.Models;


namespace HyPlayer.App.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : HomePageBase
    {
        private int selectedIndex;
       
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.GetSongsAsync().SafeFireAndForget();
            DateTime currentTime = DateTime.Now;
            int hour = currentTime.Hour;
            if (hour < 11 && hour>=6)
            {
                Greetings.Text="���Ϻã�";
                GreetingsText.Text = "�����ֿ����µ�һ���";
            }
            else if(hour>=11 && hour<13)
            {
                Greetings.Text = "����ã�";
                GreetingsText.Text = "�������ַ����������";
            }
            else if (hour >= 13 && hour < 17)
            {
                Greetings.Text = "����ã�";
                GreetingsText.Text = "�����ڶ��������ɵ��а�";
            }
            else if (hour >= 17 && hour < 23)
            {
                Greetings.Text = "���Ϻã�";
                GreetingsText.Text = "�������ֻ���һ���ƣ�Ͱ�";
            }
            else
            {
                Greetings.Text = "ҹ���ˡ�";
                GreetingsText.Text = "Ը��������ְ����������";
            }
        }


        private void OnPlaylistItemClicked(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine($"Clicking on {(e.ClickedItem as ProvidableItemBase)?.Name}");
        }
    }

    public class HomePageBase : AppPageWithScopedViewModelBase<HomeViewModel>
    {

    }
}