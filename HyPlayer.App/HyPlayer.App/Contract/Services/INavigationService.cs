using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace HyPlayer.App.Contract.Services;

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

    bool NavigateTo(Type Page, object? parameter = null);

    bool GoBack();
}
