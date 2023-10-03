using CommunityToolkit.WinUI.UI.Converters;
using Microsoft.UI.Xaml.Controls;


namespace HyPlayer.ViewModels.Converters
{
    internal class BooleanToNavButtonVisibleConverter : BoolToObjectConverter
    {
        public BooleanToNavButtonVisibleConverter() 
        {
            base.TrueValue = NavigationViewBackButtonVisible.Visible;
            base.FalseValue = NavigationViewBackButtonVisible.Collapsed;
        }

    }
    
}
