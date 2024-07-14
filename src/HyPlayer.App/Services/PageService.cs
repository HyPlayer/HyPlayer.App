using HyPlayer.Interfaces.Services;
using HyPlayer.Views.Pages;
using System;

namespace HyPlayer.Services
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
