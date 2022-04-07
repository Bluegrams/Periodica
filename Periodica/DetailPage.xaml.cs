using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Bluegrams.Periodica.ViewModels;
using System.Globalization;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Bluegrams.Periodica.Helpers;
//using Microsoft.Advertising.WinRT.UI;

namespace Bluegrams.Periodica
{
    public sealed partial class DetailPage : Page
    {
        private ElementViewModel element;
        private bool hasImages { get { return element.Images.Length > 0; } }
        private Brush PageBrush
        {
            get { return AppTheme.Instance.IsDark ? new SolidColorBrush(Colors.Black) : element.ColorBrush; }
        }
        private Brush BoxBrush
        {
            get { return AppTheme.Instance.IsDark ? element.ColorBrush : new SolidColorBrush(Color.FromArgb(150, 255, 255, 255)); }
        }

        public DetailPage()
        {
            this.InitializeComponent();
            KeyDown += DetailPage_KeyDown;
            // Enable / disable features that need upgrade
            //adOne.Visibility = App.HasUpgrade ? Visibility.Collapsed : Visibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == typeof(ElementViewModel))
            {
                element = (ElementViewModel)e.Parameter;
                this.DataContext = element;
                this.initializeView();
            }
            base.OnNavigatedTo(e);
        }

        private void initializeView()
        {
            lnkWiki.NavigateUri = new Uri(String.Format("https://{0}.wikipedia.org/wiki/{1}",
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, element.Element.LocalizedName));
            lnkWolfram.NavigateUri = new Uri(String.Format("https://www.wolframalpha.com/input/?i={0}", element.Element.EnglishName));
            lnkRSC.NavigateUri = new Uri(String.Format("https://www.rsc.org/periodic-table/element/{0}", element.Element.AtomicNumber));
            lnkWebElements.NavigateUri = new Uri(String.Format("https://www.webelements.com/{0}", element.Element.EnglishName.ToLower()));
        }

        private void DetailPage_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Left)
            {
                Frame.Navigate(typeof(DetailPage), element.PeriodicTable.GetPreviousElement(element.Element.AtomicNumber));
            }
            else if (e.Key == Windows.System.VirtualKey.Right)
            {
                Frame.Navigate(typeof(DetailPage), element.PeriodicTable.GetNextElement(element.Element.AtomicNumber));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void PreviousElementButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DetailPage), element.PeriodicTable.GetPreviousElement(element.Element.AtomicNumber));
        }

        private void NextElementButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DetailPage), element.PeriodicTable.GetNextElement(element.Element.AtomicNumber));
        }

        private void ToClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            string clipText = element.Element.ToString();
            clipText = clipText.Replace("; ", Environment.NewLine);
            DataPackage package = new DataPackage();
            package.RequestedOperation = DataPackageOperation.Copy;
            package.SetText(clipText);
            Clipboard.SetContent(package);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

//        private void AdControl_ErrorOccurred(object sender, AdErrorEventArgs e)
//        {
//#if DEBUG
//            System.Diagnostics.Debug.WriteLine("AdControl error (" + ((AdControl)sender).Name +
//                "): " + e.ErrorMessage + " ErrorCode: " + e.ErrorCode.ToString());
//#endif
//        }
    }
}
