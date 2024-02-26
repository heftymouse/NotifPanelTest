using Microsoft.UI.Xaml.Controls;

namespace NotifPanelTest
{
    public class Notification(string Title, string Description, InfoBarSeverity Severity)
    {
        public string Title = Title;
        public string Description = Description;
        public InfoBarSeverity Severity = Severity;
    }
}
