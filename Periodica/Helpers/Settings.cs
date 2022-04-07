using System;
using System.Collections.ObjectModel;
using Bluegrams.Periodica.Data;

namespace Bluegrams.Periodica.Helpers
{
    class Settings : SettingsBase
    {
        private static readonly Settings instance;

        static Settings()
        {
            instance = new Settings();
        }

        public static Settings Instance { get { return instance; } }

        private Settings()
        {
            // Indicate changed settings if an item in the collection was changed.
            ListProperties.CollectionChanged += (s, e) =>
            {
                OnSettingChanged("ListProperties");
            };
        }

        private TemperatureUnit temperatureUnit;
        private string displayedProperty = "AtomicMass";

        [Setting]
        public TemperatureUnit TemperatureUnit
        {
            get { return temperatureUnit; }
            set
            {
                temperatureUnit = value;
                OnSettingChanged(nameof(TemperatureUnit));
                OnSettingChanged(nameof(DisplayedProperty));
            }
        }

        [Setting]
        public string DisplayedProperty
        {
            get { return displayedProperty; }
            set
            {
                displayedProperty = value;
                OnSettingChanged(nameof(DisplayedProperty));
            }
        }

        /// <summary>
        /// Defines the properties of the elements that are currently displayed in list view.
        /// (format described in ElementManipulation.cs).
        /// </summary>
        [Setting]
        public ObservableCollection<string> ListProperties { get; set; } = new ObservableCollection<string>()
        {
            // IMPORTANT: First three items are mentioned here but hardcoded to this values at other places.
            "AtomicNumber",
            "Symbol",
            "LocalizedName",
            "AtomicMass",
            "temp:MeltingPoint",
            "temp:BoilingPoint",
            "Electronegativity"
        };
    }
}
