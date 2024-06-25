using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyPlayer.ViewModels.Converters
{
    class TimeStampToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            if (value is not long timeStamp) return null;
            var localDateTime = (new DateTime(1970, 1, 1).AddMilliseconds(timeStamp)).ToLocalTime();
            var now = DateTime.Now;
            var timeSpan = now - localDateTime;
            if (timeSpan.TotalDays <= 1)
            {
                if (timeSpan.TotalHours >= 1)
                {
                    return $"{(int)timeSpan.TotalHours} 小时之前";
                }if (timeSpan.TotalMinutes >= 1)
                {
                    return $"{(int)timeSpan.TotalMinutes} 分钟之前";
                }
                {
                    return "刚刚";
                }
            }
            else
            {
                return localDateTime.ToString("yyyy-MM-dd");
            }
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
