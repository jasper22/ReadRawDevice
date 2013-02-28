
namespace ReadRawDevice.Gui.ViewModel
{
    using System;
    using System.Windows;

    /// <summary>
    /// Object will allow to change VisualSate on ViewModel via attached properties
    /// </summary>
    public static class VisualStateManagerEx
    {
        private static PropertyChangedCallback callback = new PropertyChangedCallback(VisualStateChanged);

        /// <summary>
        /// Gets the state of the visual.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string GetVisualState(DependencyObject obj)
        {
            if (obj != null)
            {
                return (string)obj.GetValue(VisualStateProperty);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Sets the state of the visual.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetVisualState(DependencyObject obj, string value)
        {
            if (obj != null)
            {
                obj.SetValue(VisualStateProperty, value);
            }
        }

        /// <summary>
        /// DP for 'VisualState'
        /// </summary>
        public static readonly DependencyProperty VisualStateProperty =
            DependencyProperty.RegisterAttached(
                "VisualState",
                typeof(string),
                typeof(VisualStateManagerEx),
                new PropertyMetadata(null, VisualStateManagerEx.callback)
            );

        /// <summary>
        /// Visuals the state changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object</param>
        /// <param name="eventArgs">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void VisualStateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            //Control changeStateControl = d as Control;
            FrameworkElement changeStateControl = dependencyObject as FrameworkElement;
            if (changeStateControl == null)
            {
                throw (new ApplicationException("'VisualState' works only on Controls type"));
            }

            if (Application.Current.Dispatcher.CheckAccess() == false)
            {
                // Wrong thread
                System.Diagnostics.Debug.WriteLine("[VisualStateManagerEx] 'VisualStateChanged' event received on wrong thread -> re-route via Dispatcher");
                Application.Current.Dispatcher.BeginInvoke(
                    //() => { VisualStateChanged(d, e); }
                    VisualStateManagerEx.callback
                    , new object[] { dependencyObject, eventArgs });    //recursive
            }
            else
            {
                if (string.IsNullOrEmpty(eventArgs.NewValue.ToString()) == false)
                {
                    //VisualStateManager.GoToState(changeStateControl, e.NewValue.ToString(), true);
                    VisualStateManager.GoToElementState(changeStateControl, eventArgs.NewValue.ToString(), true);
                    System.Diagnostics.Debug.WriteLine("[VisualStateManagerEx] Visual state changed to " + eventArgs.NewValue.ToString());
                }
            }
        }
    }

}
