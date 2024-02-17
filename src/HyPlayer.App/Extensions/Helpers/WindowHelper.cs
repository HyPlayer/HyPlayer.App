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
        static public Window CreateWindow(bool high = true)
        {
            Window newWindow = new MainWindow();

            InitializeTitleBarForWindow(newWindow, high);

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



        static public Window? GetWindowForElement(UIElement element)
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

        static public void InitializeTitleBarForWindow(Window window, bool isTallTitleBar = true)
        {
            var titleBar = window.AppWindow.TitleBar;
            titleBar.BackgroundColor = Colors.Transparent;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.InactiveBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;

            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                titleBar.ExtendsContentIntoTitleBar = true;
            }

                // A taller title bar is only supported when drawing a fully custom title bar
                if (AppWindowTitleBar.IsCustomizationSupported() && titleBar.ExtendsContentIntoTitleBar)
            {
                if (isTallTitleBar)
                {
                    // Choose a tall title bar to provide more room for interactive elements 
                    // like search box or person picture controls.
                    titleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
                }
                else
                {
                    titleBar.PreferredHeightOption = TitleBarHeightOption.Standard;
                }
            }
        }

        static public List<Window> ActiveWindows { get { return _activeWindows; } }

        static private List<Window> _activeWindows = new List<Window>();
    }
}

