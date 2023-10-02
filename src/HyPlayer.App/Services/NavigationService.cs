using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using HyPlayer.Interfaces.Views;

namespace HyPlayer.Services
{
    internal class NavigationService : INavigationService
    {
        public bool CanGoBack => App.contentFrame.CanGoBack;

        public Frame? Frame => App.contentFrame;

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

        public bool NavigateTo(Page Page, object parameter)
        {
            if (Frame != null)
            {
                return Frame.Navigate(typeof(Page), parameter, new SlideNavigationTransitionInfo { Effect = SlideNavigationTransitionEffect.FromRight });
                
            }
            else
            {
                return false;
            }
        }
    }
}
