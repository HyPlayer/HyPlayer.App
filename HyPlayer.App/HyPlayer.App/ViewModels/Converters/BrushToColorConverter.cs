using System;
using System.Drawing;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace HyPlayer.App.ViewModels.Converters;

internal class BrushToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var brushConverter = new ColorConverter();
        var brush = (SolidColorBrush)value;
        return brush.Color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

