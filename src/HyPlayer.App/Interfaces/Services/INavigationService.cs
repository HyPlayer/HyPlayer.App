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

    void RegisterFrameEvents(Frame frame);

    void UnregisterFrameEvents();

    bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false);

    bool GoBack();
}
