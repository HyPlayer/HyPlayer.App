using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;

namespace HyPlayer.ViewModels.Converters;

public class StringListToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is List<string> strs)
        {
            return string.Join(" / ", strs);
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}