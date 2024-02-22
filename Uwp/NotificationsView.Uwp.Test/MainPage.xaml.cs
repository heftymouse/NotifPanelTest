using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NotificationsView.Uwp.Test
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
