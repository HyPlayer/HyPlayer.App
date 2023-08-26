using Microsoft.UI.Xaml;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using WinRT.Interop;
using HyPlayer.Views.Window;
using Microsoft.UI.Windowing;

namespace HyPlayer.Extensions.Helpers
{
    public class WindowHelper
    {
        static public Window CreateWindow()
        {
            Window newWindow = new MainWindow();

            TrackWindow(newWindow);
            return newWindow;
        }

        static public Window CreateSmallWindow()
        {
            Window newWindow = new SmallWindow();

            TrackWindow(newWindow);
            return newWindow;
        }

        static public Window CreateBlankWindow()
        {
            Window newWindow = new Window();

            TrackWindow(newWindow);
            return newWindow;
        }

        static public void TrackWindow(Window window)
        {
            window.Closed += (sender, args) => {
                _activeWindows.Remove(window);
            };
            _activeWindows.Add(window);
        }

        static public AppWindow GetAppWindow(Window window)
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(window);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }

        static public Window GetWindowForElement(UIElement element)
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

        static public List<Window> ActiveWindows { get { return _activeWindows; } }

        static private List<Window> _activeWindows = new List<Window>();
    }
}
}
