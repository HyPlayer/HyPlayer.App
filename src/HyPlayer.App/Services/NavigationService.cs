using HyPlayer.Interfaces.Services;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace HyPlayer.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IPageService _pageService;
        private object? _lastParameterUsed;
        private Frame? _frame;

        public event NavigatedEventHandler? Navigated;

        public Frame? Frame
        {
            get
            {
                return _frame;
            }


        }

        public bool CanGoBack => Frame != null && Frame.CanGoBack;

        public NavigationService(IPageService pageService)
        {
            _pageService = pageService;
        }

        public void RegisterFrameEvents(Frame frame)
        {
            _frame = frame;
            if (_frame != null)
            {
                _frame.Navigated += OnNavigated;
            }
        }

        public void UnregisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated -= OnNavigated;
            }
        }

        public bool GoBack()
        {
            if (CanGoBack)
            {             
                _frame.GoBack();
                return true;
            }

            return false;
        }

        public bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false)
        {
            var pageType = _pageService.GetPageType(pageKey);

            if (_frame != null && (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed))))
            {
                _frame.Tag = clearNavigation;
                var navigated = _frame.Navigate(pageType, parameter);
                if (navigated)
                {
                    _lastParameterUsed = parameter;

                }

                return navigated;
            }

            return false;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame frame)
            {
                var clearNavigation = (bool)frame.Tag;
                if (clearNavigation)
                {
                    frame.BackStack.Clear();
                }

                Navigated?.Invoke(sender, e);
            }
        }
    }
}
