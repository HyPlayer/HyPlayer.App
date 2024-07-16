using Microsoft.UI.Xaml.Data;
using System;

namespace HyPlayer.ViewModels.Converters
{
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var texts = ((string)parameter).Split('|');
            return (bool)value ? texts[1] : texts[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
