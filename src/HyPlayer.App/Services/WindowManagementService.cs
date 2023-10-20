using System.Collections.Generic;
using HyPlayer.Interfaces.Services;


namespace HyPlayer.Services
{
    public class WindowManagementService : IWindowManagementService
    {
        public Window Current => (_current?? new MainWindow());

        private Window? _current;

        public Window CreateWindow(bool isEnsureEarlyWindow = false)
        {
            Window newWindow = new WindowEx();

            if (isEnsureEarlyWindow) EnsureEarlyWindow(newWindow);
            TrackWindow(newWindow);
            return newWindow;
        }

        public void EnsureEarlyWindow(Window TargetWindow)
        {
            AppWindowTitleBar titleBar = TargetWindow.AppWindow.TitleBar;

            titleBar.BackgroundColor = Colors.Transparent;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.InactiveBackgroundColor = Colors.Transparent;

            titleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
            titleBar.ExtendsContentIntoTitleBar = true;
        }

        public Window? GetWindowForElement(UIElement Element)
        {
            if (Element.XamlRoot != null)
            {
                foreach (Window window in _activeWindows)
                {
                    if (Element.XamlRoot == window.Content.XamlRoot)
                    {
                        return window;
                    }
                }
            }
            return null;
        }

        public void TrackWindow(Window TargetWindow)
        {
            TargetWindow.Closed += (sender, args) => {
                _activeWindows.Remove(TargetWindow);
            };
            _activeWindows.Add(TargetWindow);
        }

        public AppWindow GetAppWindowForCrrent()
        {
            return Current.AppWindow;
        }

        public List<Window> ActiveWindows { get { return _activeWindows; } }

        private List<Window> _activeWindows = new List<Window>();
    }
}
