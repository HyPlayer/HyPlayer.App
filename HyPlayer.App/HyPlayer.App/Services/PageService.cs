using HyPlayer.App.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyPlayer.App.Interfaces.Views;

namespace HyPlayer.App.Services
{
    class PageService : IPageService
    {
        public Type GetPageType(string key)
        {
            if (key == "HomePage")
            {
                return typeof(HomePage);
            }
            else return typeof(ErrorPage);
        }
    }
}
