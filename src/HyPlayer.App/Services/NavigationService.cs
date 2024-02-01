using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using HyPlayer.Interfaces.Services;

namespace HyPlayer.Services
{
    internal class NavigationService : INavigationService
    {
        private readonly IPageService _pageService;
        private object? _lastParameterUsed;

        public NavigationService(IPageService pageService)
        {
            _pageService = pageService;
        }

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

        public bool NavigateTo(Type Page, object? parameter = null)
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

        public bool NavigateTo(Page Page, object? parameter = null)
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

        public bool NavigateTo(string pageKey, object? parameter = null)
        {
            var pageType = _pageService.GetPageType(pageKey);

            if (Frame != null && (Frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed))))
            {
                
                var navigated = Frame.Navigate(pageType, parameter);
                if (navigated)
                {
                    _lastParameterUsed = parameter;
                    
                }

                return navigated;
            }

            return false;
        }

    }
}
