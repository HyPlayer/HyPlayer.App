using Microsoft.UI.Xaml.Data;
using System;

namespace HyPlayer.ViewModels.Converters;

public class CountHumanizerConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            int numInt => ParseInt(numInt),
            long numLong => ParseLong(numLong),
            double numDouble => ParseDouble(numDouble),
            _ => value.ToString() ?? string.Empty
        };
    }

    private static string ParseInt(int value)
    {
        return value switch
        {
            < 10000 => value.ToString("D"),
            >= 10000 and < 100000000 => (value / 10000).ToString() + " 万",
            > 100000000 => (value / 10000000).ToString() + " 亿",
            _ => value.ToString()
        };
    }

    private static string ParseLong(long value)
    {
        return value switch
        {
            < 10000 => value.ToString("D"),
            >= 10000 and < 100000000 => (value / 10000).ToString() + " 万",
            > 100000000 => (value / 10000000).ToString() + " 亿",
            _ => value.ToString()
        };
    }

    private static string ParseDouble(double val)
    {
        var value = (long)val;
        return value switch
        {
            < 10000 => value.ToString("D"),
            >= 10000 and < 100000000 => (value / 10000).ToString() + " 万",
            > 100000000 => (value / 10000000).ToString() + " 亿",
            _ => value.ToString()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}