using System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace HyPlayer.Interfaces.Services
{
    public interface IWindowManagementService
    {
        Window CreateWindow(bool useHighTitleBar = true);

        Window? GetWindowForElement(UIElement element);

        void EnsureEarlyWindow(Window TargetWindow);

        void TrackWindow(Window TargetWindow);

        AppWindow GetAppWindowForCrrent();

        Window Current
        {
            get;
        }
    }
}
