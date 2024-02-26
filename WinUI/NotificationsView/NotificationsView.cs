using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace NotificationsView
{
    public partial class NotificationsView : Control
    {
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(NotificationsView), new PropertyMetadata(null));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(NotificationsView), new PropertyMetadata(null));

        private ItemsRepeater repeater;

        public event EventHandler<object> Dismissed;

        public NotificationsView() : base()
        {
            this.DefaultStyleKey = typeof(NotificationsView);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            repeater = this.GetTemplateChild("TheRepeater") as ItemsRepeater;
            repeater.ElementPrepared += OnElementPrepared;
            repeater.ElementClearing += OnElementClearing;
        }

        private void OnElementClearing(ItemsRepeater sender, ItemsRepeaterElementClearingEventArgs args)
        {
            if (args.Element is NotificationContainer container)
            {
                container.Dismissed -= Container_Dismissed;
                container.Shutdown();
            }
        }

        private void OnElementPrepared(ItemsRepeater sender, ItemsRepeaterElementPreparedEventArgs args)
        {
            if(args.Element is NotificationContainer container)
            {
                container.Dismissed += Container_Dismissed;
                container.Setup();
            }
        }

        private void Container_Dismissed(object sender, EventArgs e)
        {
            var index = repeater.GetElementIndex(sender as UIElement);
            if (index is -1)
                return;

            var item = repeater.ItemsSourceView.GetAt(index);
            Dismissed?.Invoke(this, item);
        }
    }
}
