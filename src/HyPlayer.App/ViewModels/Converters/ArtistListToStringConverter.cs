using HyPlayer.PlayCore.Abstraction.Models.Containers;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HyPlayer.ViewModels.Converters
{
    internal class ArtistListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is List<PersonBase> artistList)
            {
                return string.Join("、", artistList.Select(t => t.Name));
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}