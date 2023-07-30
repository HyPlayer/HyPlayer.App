using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using HyPlayer.App.Interfaces.Views;

namespace HyPlayer.App.Services
{
    internal class NavigationService : INavigationService
    {
        public bool CanGoBack => App.contentFrame.CanGoBack;

        public Frame Frame => App.contentFrame;

        public event NavigatedEventHandler Navigated;

        public bool GoBack()
        {
            if(CanGoBack)
            {
                Frame.GoBack();
                return true;
            }
            else return false;
        }

        public bool NavigateTo(Type Page, object parameter = null)
        {
            if (Frame != null)
            {
                var navigated = Frame.Navigate(Page, parameter, new SlideNavigationTransitionInfo { Effect = SlideNavigationTransitionEffect.FromRight });
                return navigated;
            }
            else 
            {
                return false; 
            }
        }
    }
}
