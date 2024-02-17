using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI;

namespace HyPlayer.Extensions.Helpers
{
    public class TitleBarHelper
    {
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
    }
}
