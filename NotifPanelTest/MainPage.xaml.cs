using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NotifPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Notification> mockNotifications =
           [
               new("Notification 1", "Sample description for notification 1", InfoBarSeverity.Informational),
               new("Notification 2", "Sample description for notification 2", InfoBarSeverity.Success),
               new("Notification 3", "Sample description for notification 3", InfoBarSeverity.Error),
               new("Notification 4", "Sample description for notification 4", InfoBarSeverity.Success),
               new("Notification 5", "Sample description for notification 5", InfoBarSeverity.Informational),
               new("Notification 6", "Sample description for notification 6", InfoBarSeverity.Error),
               new("Notification 7", "Sample description for notification 7", InfoBarSeverity.Success),
               new("Notification 8", "Sample description for notification 8", InfoBarSeverity.Informational),
           ];

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mockNotifications.Insert(0, new("New notification", "This is pretty cool", InfoBarSeverity.Informational));
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            mockNotifications.Clear();
        }

        private void NotificationContainer_Dismissed(object sender, object e)
        {
            mockNotifications.Remove(e as Notification);
        }
    }
}
