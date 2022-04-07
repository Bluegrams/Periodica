using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Bluegrams.Periodica.Controls;
using Bluegrams.Periodica.ViewModels;
using Windows.UI;
using Bluegrams.Periodica.Helpers;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;

namespace Bluegrams.Periodica
{
    sealed partial class MainPage : Page
    {
        public PeriodicTableViewModel Table { get; set; }
        public ColoringViewModel ColoringOptions { get; set; }

        private SearchHelper searchHelper;

        public MainPage()
        {
            Table = new PeriodicTableViewModel();
            this.InitializeComponent();
            ColoringOptions = new ColoringViewModel();
            searchHelper = new SearchHelper(Table);
            KeyDown += MainPage_KeyDown;
            ColoringOptions.PreviewOptionChanged += ColoringOptions_PreviewOptionChanged;
            // Enable / disable features that need upgrade
            butExport.Visibility = App.HasUpgrade ? Visibility.Visible : Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is int index)
            {
                pivMain.SelectedIndex = index;
            }
            base.OnNavigatedTo(e);
        }

        private void MainPage_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var ctrl = Window.Current.CoreWindow.GetKeyState(VirtualKey.Control);
            if (ctrl.HasFlag(CoreVirtualKeyStates.Down) && e.Key == VirtualKey.F)
            {
                boxFind.Focus(FocusState.Programmatic);
            }
        }

        private void PeriodicTableControl_ElementClicked(object sender, ElementEventArgs e)
        {
            Frame.Navigate(typeof(DetailPage), e.Element);
        }

        private void ToggleButton_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            if ((bool)button.IsChecked)
            {
                Color col2 = (button.Background as SolidColorBrush).Color;
                Table.AddFilter(((TextBlock)button.Content).Text,
                    (elem) => (elem.ColorBrush as SolidColorBrush).Color == col2);
            }
            else
            {
                string filterKey = ((TextBlock)((ToggleButton)sender).Content).Text;
                Table.RemoveFilter(filterKey);
            }
        }

        private void ColoringOptions_PreviewOptionChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gridViewColoring.Items.Count; i++)
            {
                ColoringOption item = ColoringOptions.ExplanationItems[i];
                item.Selected = false;
                var filterKey = item.Name;
                Table.RemoveFilter(filterKey);
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage), new int[] { 0, pivMain.SelectedIndex });
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: PRO version: Change tab number to 2 if color tab in use.
            Frame.Navigate(typeof(SettingsPage), new int[] { 1, pivMain.SelectedIndex });
        }

        private void boxFind_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (String.IsNullOrWhiteSpace(sender.Text))
                {
                    sender.ItemsSource = null;
                    searchHelper.ClearSearch();
                    return;
                }
                else sender.ItemsSource = searchHelper.SearchFor(sender.Text);
            }
        }

        private void boxFind_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                Frame.Navigate(typeof(DetailPage), args.ChosenSuggestion);
                sender.Text = "";
                searchHelper.ClearSearch();
            }
            else
            {
                searchHelper.ApplySearch(sender.Text);
                pivMain.SelectedIndex = 1;
            }
        }

        private void pivMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pivMain.SelectedIndex == 0)
                searchHelper.ClearSearch();
        }

        private void butClearSearch_Click(object sender, RoutedEventArgs e)
        {
            searchHelper.ClearSearch();
        }

        private void butExport_Click(object sender, RoutedEventArgs e)
        {
            periodicTableView.ExportPeriodicTableView();
        }

        private async void rateButton_Click(object sender, RoutedEventArgs e)
        {
            var rateUri = new Uri("ms-windows-store://review/?ProductId=" + "9PB2TD7P6DT3");
            await Windows.System.Launcher.LaunchUriAsync(rateUri);
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            periodicTableView.ZoomFactor -= 0.1f;
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            periodicTableView.ZoomFactor += 0.1f;
        }

        private async void donateButton_Click(object sender, RoutedEventArgs e)
        {
            var donateUri = new Uri("https://ko-fi.com/alxnull");
            await Launcher.LaunchUriAsync(donateUri);
        }
    }
}
