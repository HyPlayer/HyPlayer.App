using HyPlayer.Helpers;
using HyPlayer.Interfaces.Services;
using HyPlayer.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyPlayer.Services
{
    class NavigationViewService : INavigationViewService
    {
        private readonly INavigationService _navigationService;
        private readonly IPageService _pageService;
        private NavigationView? _navigationView;

        public NavigationViewService(INavigationService navigationService, IPageService pageService)
        {
            _navigationService = navigationService;
            _pageService = pageService;
        }

        public IList<object>? MenuItems => _navigationView.MenuItems;

        public object? SettingsItem => _navigationView.SettingsItem;

        public NavigationViewItem? GetSelectedItem(Type pageType)
        {
            throw new NotImplementedException();
        }

        public void Initialize(NavigationView navigationView)
        {
            _navigationView = navigationView;
            navigationView.ItemInvoked += OnItemInvoked;
            navigationView.BackRequested += OnBackRequested;
        }

        public void UnregisterEvents()
        {
            throw new NotImplementedException();
        }

        private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => _navigationService.GoBack();

        private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                _navigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
            }
            else
            {
                var selectedItem = args.InvokedItemContainer as NavigationViewItem;

                if (selectedItem?.GetValue(NavigationHelper.NavigateToProperty) is string pageKey)
                {
                    _navigationService.NavigateTo(pageKey);
                }
            }
        }
    }
}
