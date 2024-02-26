using Microsoft.UI.Composition;
using Microsoft.UI.Composition.Interactions;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NotificationsView
{
    public sealed partial class NotificationContainer : ContentControl, IInteractionTrackerOwner
    {
        public event EventHandler Dismissed;

        InteractionTracker tracker;
        VisualInteractionSource source;
        Visual visual;
        UIElement root;

        bool initialized;

        public NotificationContainer()
        {
            this.DefaultStyleKey = typeof(NotificationContainer);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Loaded += NotificationContainer_Loaded;
            
        }

        private void NotificationContainer_Loaded(object sender, RoutedEventArgs e)
        {
            Setup();

            PointerEventHandler handler = new(TheInfoBar_PointerPressed);
            root.AddHandler(PointerPressedEvent, handler, true);
        }

        public void Setup()
        {
            if (initialized || !this.IsLoaded)
                return;

            root = this;
            visual = ElementCompositionPreview.GetElementVisual(root);
            visual.Opacity = 1;
            ElementCompositionPreview.SetIsTranslationEnabled(root, true);

            tracker = InteractionTracker.CreateWithOwner(visual.Compositor, this);
            tracker.MinPosition = new(-0.01f);
            source = VisualInteractionSource.Create(visual);
            source.ManipulationRedirectionMode = VisualInteractionSourceRedirectionMode.CapableTouchpadOnly;
            source.PositionXSourceMode = InteractionSourceMode.EnabledWithInertia;
            source.PositionYSourceMode = InteractionSourceMode.Disabled;
            tracker.InteractionSources.Add(source);

            var anim = visual.Compositor.CreateExpressionAnimation("-tracker.Position.X");
            anim.SetReferenceParameter("tracker", tracker);
            visual.StartAnimation("Translation.X", anim);

            initialized = true;
        }

        public void Shutdown()
        {
            if(!initialized)
                return;

            visual.StopAnimation("Translation.X");
            visual.Offset = new(0);
            source.Dispose();
            source = null;
            tracker.Dispose();
            tracker = null;
            visual = null;
            initialized = false;
        }

        private void TheInfoBar_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if(e.Pointer.PointerDeviceType == PointerDeviceType.Touch)
            {
                source.TryRedirectForManipulation(e.GetCurrentPoint(root));
            }
        }

        public void InertiaStateEntered(InteractionTracker sender, InteractionTrackerInertiaStateEnteredArgs args)
        {
            if(sender.Position.X < -40 || args.PositionVelocityInPixelsPerSecond.X < -2000)
            {
                Dismissed?.Invoke(this, null);
            }
        }

        public void CustomAnimationStateEntered(InteractionTracker sender, InteractionTrackerCustomAnimationStateEnteredArgs args) { }
        public void IdleStateEntered(InteractionTracker sender, InteractionTrackerIdleStateEnteredArgs args) { }
        public void InteractingStateEntered(InteractionTracker sender, InteractionTrackerInteractingStateEnteredArgs args) { }
        public void RequestIgnored(InteractionTracker sender, InteractionTrackerRequestIgnoredArgs args) { }
        public void ValuesChanged(InteractionTracker sender, InteractionTrackerValuesChangedArgs args) { }
    }
}
