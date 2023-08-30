using CommunityToolkit.WinUI.UI.Converters;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
