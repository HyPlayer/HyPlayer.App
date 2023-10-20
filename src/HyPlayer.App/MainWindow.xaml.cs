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
