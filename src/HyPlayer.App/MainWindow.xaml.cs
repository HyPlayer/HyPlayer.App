using System;

namespace HyPlayer
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WindowEx
    {
        public MainWindow()
        {
            this.InitializeComponent();
            App.themeListener.ThemeChanged += MainWindow_ThemeChanged;
        }

        private void MainWindow_ThemeChanged(CommunityToolkit.WinUI.Helpers.ThemeListener sender)
        {
            var titleBar = AppWindow.TitleBar;

            if (sender.CurrentTheme == ApplicationTheme.Light) 
            {
                titleBar.ForegroundColor = Colors.Black;
            }
        }

        public Frame GetRootFrame()
        {
            Frame rootFrame = Content as Frame;
            if (Content is null)
            {
                rootFrame = new Frame();
                Content = rootFrame;
            }
            return rootFrame;
        }
    }
}
