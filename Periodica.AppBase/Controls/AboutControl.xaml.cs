using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Bluegrams.Periodica.Controls
{
    [ContentProperty(Name = "DescriptionContent")]
    public sealed partial class AboutControl : UserControl
    {
        private Package package;

        public string PackageVersion
        {
            get
            {
                return String.Format("{0}.{1}.{2}", 
                    package.Id.Version.Major, package.Id.Version.Minor, package.Id.Version.Build);
            }
        }

        public string DisplayName { get { return package.DisplayName; } }

        public string Publisher { get { return package.PublisherDisplayName; } }

        public static readonly DependencyProperty WebsiteProperty =
            DependencyProperty.Register("Website", typeof(string), typeof(AboutControl), new PropertyMetadata(null));

        public string Website
        {
            get { return (string)GetValue(WebsiteProperty); }
            set { SetValue(WebsiteProperty, value); }
        }

        public static readonly DependencyProperty StoreIDProperty =
            DependencyProperty.Register("StoreID", typeof(string), typeof(AboutControl), new PropertyMetadata(null));

        public string StoreID
        {
            get { return (string)GetValue(StoreIDProperty); }
            set { SetValue(StoreIDProperty, value); }
        }

        public static readonly DependencyProperty MailToProperty =
            DependencyProperty.Register("MailTo", typeof(string), typeof(AboutControl), new PropertyMetadata(null));

        public string MailTo
        {
            get { return (string)GetValue(MailToProperty); }
            set { SetValue(MailToProperty, value); }
        }

        public static readonly DependencyProperty DescriptionContentProperty =
            DependencyProperty.Register("DescriptionContent", typeof(object), typeof(AboutControl), new PropertyMetadata(null));

        public object DescriptionContent
        {
            get { return GetValue(DescriptionContentProperty); }
            set { SetValue(DescriptionContentProperty, value); }
        }

        public AboutControl()
        {
            package = Package.Current;
            this.InitializeComponent();
            if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                this.feedbackButton.Visibility = Visibility.Visible;
            }
        }

        private async void MailToButton_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("mailto:" + MailTo);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private async void RateButton_Click(object sender, RoutedEventArgs e)
        {
            var rateUri = new Uri("ms-windows-store://review/?ProductId=" + StoreID);
            await Windows.System.Launcher.LaunchUriAsync(rateUri);
        }

        private async void feedbackButton_Click(object sender, RoutedEventArgs e)
        {
            var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }

        private async void DonateButton_Click(object sender, RoutedEventArgs e)
        {
            var donateUri = new Uri("https://ko-fi.com/alxnull");
            await Windows.System.Launcher.LaunchUriAsync(donateUri);
        }
    }
}
