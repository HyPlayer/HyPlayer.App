using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using HyPlayer.Views.Controls.Dialogs;
using HyPlayer.Views.Pages;
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


namespace HyPlayer.Views.Window
{
    public sealed partial class MainWindow : WindowEx
    {
      
        private readonly AppWindowTitleBar titleBar;

        private static MainWindow? _Instance;

        public static MainWindow Instance => _Instance ??= new();

        public MainWindow()
        {
            InitializeComponent();

            Activated += EnsureWindowIsInitialized;

            
            titleBar = AppWindow.TitleBar;

            EnsureEarlyWindow();
        }

        private void EnsureWindowIsInitialized(object sender, WindowActivatedEventArgs args)
        {
            // Navigate to ShellPage
            rootFrame.Navigate(typeof(ShellPage));
        }

        private void EnsureEarlyWindow()
        {
            // Set PersistenceId
            PersistenceId = "HyPlayerMainWindow";

            // Set minimum sizes
            MinHeight = 416;
            MinWidth = 516;

            
        }
    }
}
