using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyPlayer.Interfaces.Services
{
    interface INavigationViewService
    {
        IList<object>? MenuItems
        {
            get;
        }

        object? SettingsItem
        {
            get;
        }

        void Initialize(NavigationView navigationView);

        void UnregisterEvents();

        NavigationViewItem? GetSelectedItem(Type pageType);
    }
}
