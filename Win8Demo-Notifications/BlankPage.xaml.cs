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
        /// Raises a text toast notification locally.
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

        /// <summary>
        /// Raises a image toast notification locally.
        /// IMPORTANT: if copying this into your own application, ensure that "Toast capable" is enabled in the application manifest
        /// </summary>
        private void NotificationImageButton_Click(object sender, RoutedEventArgs e)
        {
            // It is possible to start from an existing template and modify what is needed.
            // Alternatively you can construct the XML from scratch.
            var toastXml = new XmlDocument();
            var title = toastXml.CreateElement("toast");
            var visual = toastXml.CreateElement("visual");
            visual.SetAttribute("version", "1");
            visual.SetAttribute("lang", "en-US");

            // The template is set to be a ToastImageAndText01. This tells the toast notification manager what to expect next.
            var binding = toastXml.CreateElement("binding");
            binding.SetAttribute("template", "ToastImageAndText01");

            // An image element is then created under the ToastImageAndText01 XML node. The path to the image is specified
            var image = toastXml.CreateElement("image");
            image.SetAttribute("id", "1");
            image.SetAttribute("src", @"Assets/DemoImage.png");

            // A text element is created under the ToastImageAndText01 XML node.
            var text = toastXml.CreateElement("text");
            text.SetAttribute("id", "1");
            text.InnerText = "Another sample toast. This time with an image";

            // All the XML elements are chained up together.
            title.AppendChild(visual);
            visual.AppendChild(binding);
            binding.AppendChild(image);
            binding.AppendChild(text);

            toastXml.AppendChild(title);

            // Create a ToastNotification from our XML, and send it to the Toast Notification Manager
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
