using HyPlayer.Views.Pages;
using HyPlayer.ViewModels;
using System;
using HyPlayer.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Windows.UI.ApplicationSettings;
using System.Linq;

namespace HyPlayer.Services
{
    class PageService : IPageService
    {
        private readonly Dictionary<string, Type> _pages = new();

        public PageService()
        {
            ConfigurePage<HomeViewModel, HomePage>();
            ConfigurePage<SearchViewModel, SearchPage>();
            ConfigurePage<NeteasePlaylistViewModel, PlaylistPage>();
            ConfigurePage<UserViewModel, UserPage>();
            ConfigurePage<SettingsViewModel, SettingsPage>();
        }

        public Type GetPageType(string key)
        {
            Type? pageType;
            lock (_pages)
            {
                if (!_pages.TryGetValue(key, out pageType))
                {
                    throw new ArgumentException($"Page not found: {key}. ");
                }
            }

            return pageType;
        }

        private void ConfigurePage<VM, V>()
            where VM : ObservableObject
            where V : Page
        {
            lock (_pages)
            {
                var key = typeof(VM).FullName!;
                if (_pages.ContainsKey(key))
                {
                    throw new ArgumentException($"The key {key} is already configured in PageService");
                }

                var type = typeof(V);
                if (_pages.ContainsValue(type))
                {
                    throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
                }

                _pages.Add(key, type);
            }
        }
    }
}
