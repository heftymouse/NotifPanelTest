using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationsView.Uwp.Test
{
    public class Notification(string Title, string Description, InfoBarSeverity Severity)
    {
        public string Title = Title;
        public string Description = Description;
        public InfoBarSeverity Severity = Severity;
    }
}
