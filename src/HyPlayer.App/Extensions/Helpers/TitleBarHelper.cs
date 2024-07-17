using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI;
using System.Runtime.InteropServices;
using System;


namespace HyPlayer.Extensions.Helpers
{
    public class TitleBarHelper
    {
        public static void InitializeTitleBarForWindow(Window window, bool useTallTitleBar = true)
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
                if (useTallTitleBar)
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
            UpdateTitleBarContextMenu(ElementTheme.Default);
        }

        private enum PreferredAppMode
        {
            Default,
            AllowDark,
            ForceDark,
            ForceLight,
            Max
        };

        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        private static extern IntPtr SetPreferredAppMode(PreferredAppMode preferredAppMode);

        [DllImport("uxtheme.dll", EntryPoint = "#136")]
        private static extern IntPtr FlushMenuThemes();

        public static void UpdateTitleBarContextMenu(Microsoft.UI.Xaml.ElementTheme theme)
        {
            var mode = theme switch
            {
                ElementTheme.Light => PreferredAppMode.ForceLight,
                ElementTheme.Dark => PreferredAppMode.ForceDark,
                _ => PreferredAppMode.AllowDark,
            };
            SetPreferredAppMode(mode);
            FlushMenuThemes();
        }
    }
}
