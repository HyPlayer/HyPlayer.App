using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;

namespace HyPlayer.Interfaces.Services
{
    public interface INavigationService
    {
        event NavigatedEventHandler Navigated;
    
        bool CanGoBack
        {
            get;
        }

        Frame? Frame
        {
            get;
        }

        bool NavigateTo(Page Page, object? parameter = null);

        bool GoBack();

        
}
}
