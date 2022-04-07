using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Bluegrams.Periodica.Helpers;
using Bluegrams.Periodica.ViewModels;
using Bluegrams.Periodica.Data;
using System.Linq;
using Windows.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.ApplicationModel.Resources;

namespace Bluegrams.Periodica
{
    public sealed partial class SettingsPage : Page
    {
        private bool needsCacheClear = false;
        private int returnParam = -1;

        private IEnumerable<Language> AppLanguageOptions { get { return AppLanguage.Instance.AppLanguageOptions; } }
        private IEnumerable<ComboBoxOption> ElementPropertyOptions { get; set; }

        public SettingsPage()
        {
            this.InitializeComponent();
            loadElementPropertyOptions();
            this.DataContext = Settings.Instance;
            AppTheme.Instance.ThemeChanged += Instance_ThemeChanged;
            AppLanguage.Instance.PropertyChanged += AppLanguage_PropertyChanged;
            // Enable / disable features that need upgrade
            //stackListProperties.Visibility = App.HasUpgrade ? Visibility.Visible : Visibility.Collapsed;
        }

        private void loadElementPropertyOptions()
        {
            ResourceLoader loader = ResourceLoader.GetForViewIndependentUse();
            ElementPropertyOptions = new List<ComboBoxOption>()
            {
                new ComboBoxOption() { Value="LocalizedName",     Content=loader.GetString("settingsPropLocalizedName/Content")},
                new ComboBoxOption() { Value="AtomicMass",        Content=loader.GetString("settingsPropAtomicMass/Content")},
                new ComboBoxOption() { Value="Density",           Content=loader.GetString("settingsPropDensity/Content") },
                new ComboBoxOption() { Value="temp:MeltingPoint", Content=loader.GetString("settingsPropMeltingPoint/Content") },
                new ComboBoxOption() { Value="temp:BoilingPoint", Content=loader.GetString("settingsPropBoilingPoint/Content") },
                new ComboBoxOption() { Value="HeatCapacity",      Content=loader.GetString("settingsPropHeatCapacity/Content") },
                new ComboBoxOption() { Value="AtomicRadius",      Content=loader.GetString("settingsPropAtomicRadius/Content") },
                new ComboBoxOption() { Value="AbundanceCrust",    Content=loader.GetString("settingsPropAbundanceCrust/Content") },
                new ComboBoxOption() { Value="AbundanceUniverse", Content=loader.GetString("settingsPropAbundanceUniverse/Content") },
                new ComboBoxOption() { Value="Electronegativity", Content=loader.GetString("settingsPropElectronegativity/Content") }
            };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            comLanguage.SetBinding(ComboBox.SelectedIndexProperty, new Binding()
            {
                Source = AppLanguage.Instance,
                Path = new PropertyPath("SelectedOption"),
                Mode = BindingMode.TwoWay
            });
            toggleCustomLang.SetBinding(ToggleSwitch.IsOnProperty, new Binding()
            {
                Source = AppLanguage.Instance,
                Path = new PropertyPath("UseSystemPreference"),
                Converter = new InvertedBoolConverter(),
                Mode = BindingMode.TwoWay
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is int index)
            {
                pivotMain.SelectedIndex = index;
            }
            else if (e.Parameter is int[] args)
            {
                pivotMain.SelectedIndex = args[0];
                returnParam = args[1];
            }
            base.OnNavigatedTo(e);
        }

        private void Instance_ThemeChanged(object sender, EventArgs e)
        {
            needsCacheClear = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            applySettings();
            navigateBack();
        }

        private void navigateBack()
        {
            if (needsCacheClear)
            {
                var backType = Frame.BackStack[Frame.BackStack.Count - 1];
                int cache = Frame.CacheSize;
                Frame.CacheSize = 0;
                Frame.CacheSize = cache;
                Frame.BackStack.Add(backType);
#if DEBUG
                System.Diagnostics.Debug.WriteLine("SettingsPage.xaml.cs: Cache cleared.");
#endif
            }
            if (returnParam > -1) Frame.Navigate(typeof(MainPage), returnParam);
            else Frame.GoBack();
        }

        private void applySettings()
        {
            var tempUnit =
                (bool)radTempCelsius.IsChecked ? TemperatureUnit.Celsius
                : (bool)radTempFahrenheit.IsChecked ? TemperatureUnit.Fahrenheit
                : TemperatureUnit.Kelvin;
            if (Settings.Instance.TemperatureUnit != tempUnit)
                Settings.Instance.TemperatureUnit = tempUnit;
        }

        private async void AppLanguage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            lblLangNotice.Opacity = 1;
            if (e.PropertyName != "SelectedOption") return;
            await Task.Delay(100);
            if (comLanguage.SelectedItem == null) return;
        }
    }

    class ComboBoxOption
    {
        public string Content { get; set; }
        public string Value { get; set; }
    }
}
