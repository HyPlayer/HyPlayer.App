using System;
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
            var result = hasCover.GetCover().GetAwaiter().GetResult()?.GetResource(new ImageResourceQualityTag(size,size), typeof(string)).GetAwaiter().GetResult();
            if (result is string url)
                return new BitmapImage(new Uri(url));
        }

        // TODO: Change the default Image Here
        return _defaultImage ??= new BitmapImage(new Uri("https://cdn.jsdelivr.net/gh/kengwang/CDN@main/avatar.png"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}