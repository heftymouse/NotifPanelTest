using Microsoft.UI.Xaml;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dwm;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NotifPanelTest
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            this.InitializeComponent();

            unsafe
            {
                int yes = 1;
                PInvoke.DwmSetWindowAttribute(
                    (HWND)WindowNative.GetWindowHandle(this),
                    DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE,
                    &yes,
                    sizeof(int)
                );
            }
        }

    }
}
