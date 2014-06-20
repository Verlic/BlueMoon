namespace BlueMoon.UI.Components
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    public static class WebBrowserHelper
    {

        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
            "Html",
            typeof(string),
            typeof(WebBrowserHelper),
            new PropertyMetadata(OnHtmlChanged));

        public static string GetHtml(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browser = d as WebBrowser;
            if (browser == null)
            {
                return;
            }

#if DEBUG
            var stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            var html = e.NewValue.ToString();
            browser.NavigateToString(html);
#if DEBUG
            stopwatch.Stop();
            Trace.TraceInformation("Navigate To String time: " + stopwatch.ElapsedMilliseconds);
#endif
        }
    }
}
