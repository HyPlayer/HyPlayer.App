using HyPlayer.Views.Window;
using HyPlayer.Models.Enums;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System.Collections.Generic;

namespace HyPlayer.Extensions.Helpers
{
    public class WindowHelper
    {
        public static List<Window> ActiveWindows { get { return _activeWindows; } }
        private static List<Window> _activeWindows = new List<Window>();

        public static Window CreateWindow(NewWindowKind newWindowKind)
        {
            Window window = null;

            switch (newWindowKind)
            {
                case NewWindowKind.Main:
                    window = new MainWindow();
                    TrackWindow(window);
                    return window;
                case NewWindowKind.Blank:
                    window = new Window();
                    TrackWindow(window);
                    return window;
                case NewWindowKind.Compact:
                    window = new CompactWindow();
                    TrackWindow(window);
                    return window;
            }

            window = new Window();
            TrackWindow(window);
            return window;
        }

        public static void TrackWindow(Window window)
        {
            window.Closed += (sender, args) =>
            {
                _activeWindows.Remove(window);
            };
            _activeWindows.Add(window);
        }


        public static Window? GetWindowForElement(UIElement element)
        {
            if (element.XamlRoot != null)
            {
                foreach (Window window in _activeWindows)
                {
                    if (element.XamlRoot == window.Content.XamlRoot)
                    {
                        return window;
                    }
                }
            }
            return null;
        }
    }
}

