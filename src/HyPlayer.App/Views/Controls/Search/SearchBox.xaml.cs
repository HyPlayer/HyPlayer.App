using HyPlayer.Views.Pages;
using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using Microsoft.UI.Xaml.Input;


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
