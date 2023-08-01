using System;
using System.Numerics;
using Humanizer;
using Microsoft.UI.Xaml.Data;

namespace HyPlayer.App.ViewModels.Converters;

public class CountHumanizerConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
               {
                   int numInt   => numInt.ToMetric(),
                   long numLong => ((double)numLong).ToMetric(),
                   _            => value.ToString() ?? string.Empty
               };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}