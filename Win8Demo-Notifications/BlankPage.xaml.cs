using System;
using System.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win8Demo_Notifications
{
    public sealed partial class BlankPage : Page
    {
        public BlankPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Raises a text notification toast locally.
        /// IMPORTANT: if copying this into your own application, ensure that "Toast capable" is enabled in the application manifest
        /// </summary>
        private void NotificationTextButton_Click(object sender, RoutedEventArgs e)
        {
            // Toasts use a predefined set of standard templates to display their content.
            // The updates happen by sending a XML fragment to the Toast Notification Manager.
            // To make things easier, we will get the template for a toast iwth text as a base, and modify it from there
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

            // Find the 'text' element in the template's XML, and insert the text "A sample toast" into it.
            var elements = toastXml.GetElementsByTagName("text");
            elements[0].AppendChild(toastXml.CreateTextNode("A sample toast"));

            // Create a ToastNotification from our XML, and send it to the Toast Notification Manager
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
