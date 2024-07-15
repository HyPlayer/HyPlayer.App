using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.Interfaces.Services;
using HyPlayer.ViewModels;
using HyPlayer.Views.Controls.Dialogs;
using HyPlayer.Views.Pages;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WinRT.Interop;
using WinUIEx;
using AppWindow = Microsoft.UI.Windowing.AppWindow;
using AppWindowTitleBar = Microsoft.UI.Windowing.AppWindowTitleBar;

namespace HyPlayer.Views.Window
{
    public sealed partial class MainWindow : WindowEx
    {
        public static new MainWindow Current => _Current ?? new MainWindow();
        private static MainWindow _Current;

        public MainWindow()
        {
            InitializeComponent();
        }

    }
}
