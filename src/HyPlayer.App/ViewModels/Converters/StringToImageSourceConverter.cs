using HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

namespace HyPlayer.ViewModels.Converters
{
    class StringToImageSourceConverter : IValueConverter
    {
        private ImageSource? _defaultImage;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new BitmapImage(new Uri((string)value));     
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
