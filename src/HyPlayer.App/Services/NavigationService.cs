using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using HyPlayer.Interfaces.Services;

namespace HyPlayer.Services
{
    internal class NavigationService : INavigationService
    {
        public bool CanGoBack => Frame.CanGoBack;

        public Frame? Frame { get; set; }

        public event NavigatedEventHandler? Navigated;

        public bool GoBack()
        {
            if(CanGoBack)
            {
                Frame?.GoBack();
                return true;
            }
            else return false;
        }

        public bool NavigateTo(Type Page, object parameter)
        {
            if (Frame != null)
            {
                return Frame.Navigate(Page, parameter, new SlideNavigationTransitionInfo { Effect = SlideNavigationTransitionEffect.FromRight });
            }
            else 
            {
                return false; 
            }
        }
    }
}
