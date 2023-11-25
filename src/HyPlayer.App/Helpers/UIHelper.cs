using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HyPlayer.Helpers
{
    public class UIHelper
    {
        public static async Task<ContentDialogResult> ShowDialogAsync(ContentDialog dialog, XamlRoot xamlRoot)
        {

            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"];
            dialog.XamlRoot = xamlRoot;

            var result = await dialog.ShowAsync();
            return result;
        }
    }
}
