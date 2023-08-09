using System;
using HyPlayer.App.Extensions;
using HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;
using HyPlayer.PlayCore.Abstraction.Models.Resources;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace HyPlayer.App.ViewModels.Converters;

public class CoverToImageSourceConverter : IValueConverter
{

    private ImageSource? _defaultImage;
    
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is IHasCover hasCover)
        {
            if (parameter is not string sizeStr)
                sizeStr = "250";
            var size = System.Convert.ToInt32(sizeStr);
            var result = hasCover.GetCoverUrl(size, size);
            if (result is not null)
                return new BitmapImage(new Uri(result));
        }

        // TODO: Change the default Image Here
        return _defaultImage ??= new BitmapImage(new Uri("https://cdn.jsdelivr.net/gh/kengwang/CDN@main/avatar.png"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}