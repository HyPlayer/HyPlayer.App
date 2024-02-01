using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace HyPlayer.Interfaces.Services;

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

    bool NavigateTo(Page Page, object? parameter = null);

    bool NavigateTo(string pageKey, object? parameter = null);

    bool GoBack();
}
