using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using WinRT.Interop;
using WinUIEx;

using AppWindow = Microsoft.UI.Windowing.AppWindow;
using AppWindowTitleBar = Microsoft.UI.Windowing.AppWindowTitleBar;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.WindowManagement;
using HyPlayer.App.Interfaces.Views;
using HyPlayer.App.ViewModels;
using Depository.Abstraction.Interfaces;
using Depository.Abstraction.Models.Options;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.App.Views.Controls.Dialogs;
using HyPlayer.App.Views.Pages;

namespace HyPlayer.App.Views.Window
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WindowEx
    {
        private ShellViewModel _shellViewModel;
        private AccountViewModel _accountViewModel;
        private readonly IDepositoryResolveScope _scope;

        public MainWindow()
        {
            this.InitializeComponent();

            _scope = DepositoryResolveScope.Create();
            _shellViewModel = App.GetDIContainer().ResolveInScope<ShellViewModel>(_scope);
            _accountViewModel = App.GetDIContainer().ResolveInScope<AccountViewModel>(_scope);

            // Customize the Title Bar.
            var titleBar = this.AppWindow.TitleBar;
            titleBar.BackgroundColor = Colors.Transparent;
            titleBar.ButtonBackgroundColor = Colors .Transparent;
            titleBar.InactiveBackgroundColor = Colors .Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors .Transparent;
            titleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;

            // Get caption button occlusion information.
            int CaptionButtonOcclusionWidthRight = titleBar.RightInset;
            int CaptionButtonOcclusionWidthLeft = titleBar.LeftInset;

            // Set the width of padding columns in the UI.
            RightPaddingColumn.Width = new GridLength(CaptionButtonOcclusionWidthRight);
            LeftPaddingColumn.Width = new GridLength(CaptionButtonOcclusionWidthLeft);
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                titleBar.ExtendsContentIntoTitleBar = true;
                AppTitleBar.Loaded += AppTitleBar_Loaded;
                AppTitleBar.SizeChanged += AppTitleBar_SizeChanged;
            }
            else
            {
                // In the case that title bar customization is not supported, hide the custom title bar
                // element.
                AppTitleBar.Visibility = Visibility.Collapsed;

                // Show alternative UI for any functionality in
                // the title bar, such as search.
            }

            // Use High TitleBar.
            bool isTallTitleBar = true;

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
                // Recalculate the drag region for the custom title bar 
                // if you explicitly defined new draggable areas.
                SetDragRegionForCustomTitleBar(AppWindow);
            }

            // Set the rootFrame of the MainWindow as App.rootFrame.
            App.contentFrame = NavView.ContentFrame;

            this.AppWindow.Changed += AppWindow_Changed;
        }

        private void AppWindow_Changed(AppWindow sender, Microsoft.UI.Windowing.AppWindowChangedEventArgs args)
        {
            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
            // Windows App SDK on Windows 11.
            if (args.DidPresenterChange
                && AppWindowTitleBar.IsCustomizationSupported())
            {
                switch (sender.Presenter.Kind)
                {
                    case AppWindowPresenterKind.CompactOverlay:
                        // Compact overlay - hide custom title bar
                        // and use the default system title bar instead.
                        AppTitleBar.Visibility = Visibility.Collapsed;
                        sender.TitleBar.ResetToDefault();
                        break;

                    case AppWindowPresenterKind.FullScreen:
                        // Full screen - hide the custom title bar
                        // and the default system title bar.
                        AppTitleBar.Visibility = Visibility.Collapsed;
                        sender.TitleBar.ExtendsContentIntoTitleBar = true;
                        break;

                    case AppWindowPresenterKind.Overlapped:
                        // Normal - hide the system title bar
                        // and use the custom title bar instead.
                        AppTitleBar.Visibility = Visibility.Visible;
                        sender.TitleBar.ExtendsContentIntoTitleBar = true;
                        //SetDragRegionForCustomTitleBar(sender);
                        break;

                    default:
                        // Use the default system title bar.
                        sender.TitleBar.ResetToDefault();
                        break;
                }
            }
        }

        private void AppTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
            // Windows App SDK on Windows 11.
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                SetDragRegionForCustomTitleBar(this.AppWindow);
            }
        }

        private void AppTitleBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
            // Windows App SDK on Windows 11.
            if (AppWindowTitleBar.IsCustomizationSupported()
                && this.AppWindow.TitleBar.ExtendsContentIntoTitleBar)
            {
                // Update drag region if the size of the title bar changes.
                SetDragRegionForCustomTitleBar(this.AppWindow);
            }
        }

        [DllImport("Shcore.dll", SetLastError = true)]
        internal static extern int GetDpiForMonitor(IntPtr hmonitor, Monitor_DPI_Type dpiType, out uint dpiX, out uint dpiY);

        internal enum Monitor_DPI_Type : int
        {
            MDT_Effective_DPI = 0,
            MDT_Angular_DPI = 1,
            MDT_Raw_DPI = 2,
            MDT_Default = MDT_Effective_DPI
        }

        private double GetScaleAdjustment()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
            IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

            // Get DPI.
            int result = GetDpiForMonitor(hMonitor, Monitor_DPI_Type.MDT_Default, out uint dpiX, out uint _);
            if (result != 0)
            {
                throw new Exception("Could not get DPI for monitor.");
            }

            uint scaleFactorPercent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
            return scaleFactorPercent / 100.0;
        }

        private void SetDragRegionForCustomTitleBar(AppWindow appWindow)
        {
            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
            // Windows App SDK on Windows 11.
            if (AppWindowTitleBar.IsCustomizationSupported()
                && appWindow.TitleBar.ExtendsContentIntoTitleBar)
            {
                double scaleAdjustment = GetScaleAdjustment();

                RightPaddingColumn.Width = new GridLength(appWindow.TitleBar.RightInset / scaleAdjustment);
                LeftPaddingColumn.Width = new GridLength(appWindow.TitleBar.LeftInset / scaleAdjustment);

                List<Windows.Graphics.RectInt32> dragRectsList = new();

                Windows.Graphics.RectInt32 dragRectL;
                dragRectL.X = (int)((LeftPaddingColumn.ActualWidth) * scaleAdjustment);
                dragRectL.Y = 0;
                dragRectL.Height = (int)(AppTitleBar.ActualHeight * scaleAdjustment);
                dragRectL.Width = (int)((IconColumn.ActualWidth
                                        + TitleColumn.ActualWidth
                                        + LeftDragColumn.ActualWidth) * scaleAdjustment);
                dragRectsList.Add(dragRectL);

                Windows.Graphics.RectInt32 dragRectR;
                dragRectR.X = (int)((LeftPaddingColumn.ActualWidth
                                    + IconColumn.ActualWidth
                                    + TitleTextBlock.ActualWidth
                                    + LeftDragColumn.ActualWidth
                                    + SearchColumn.ActualWidth) * scaleAdjustment);
                dragRectR.Y = 0;
                dragRectR.Height = (int)(AppTitleBar.ActualHeight * scaleAdjustment);
                dragRectR.Width = (int)(RightDragColumn.ActualWidth * scaleAdjustment);
                dragRectsList.Add(dragRectR);

                Windows.Graphics.RectInt32[] dragRects = dragRectsList.ToArray();

                appWindow.TitleBar.SetDragRectangles(dragRects);
            }
        }

        private void NavView_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            App.GetService<INavigationService>().GoBack();
        }


        private void GlobalTeachingTip_CloseButtonClick(TeachingTip sender, object args)
        {
            globalTeachingTip.IsOpen = false;
        }

        private async void UserButton_Click(object sender, RoutedEventArgs e)
        {
            if(_accountViewModel._provider.LoginedUser == null)
            {
                var signin_dialog = new SignInDialog();
                signin_dialog.XamlRoot = this.Content.XamlRoot;
                var result = await signin_dialog.ShowAsync();
            }
            else
            {
                App.GetService<INavigationService>().NavigateTo(typeof(MePage));
            }
        }

        
    }
}