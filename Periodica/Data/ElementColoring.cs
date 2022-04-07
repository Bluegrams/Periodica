using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Bluegrams.Periodica.Data
{
    /// <summary>
    /// This class holds the currently used coloring mode.
    /// </summary>
    public sealed class ElementColoring : INotifyPropertyChanged
    {
        private static readonly ElementColoring instance;

        static ElementColoring()
        {
            instance = new ElementColoring();
        }

        public static ElementColoring Instance { get { return instance; } }

        private UISettings uiSettings;
        private Color sysAccentColor;

        private ColoringMode currentColoring;
        /// <summary>
        /// The currently used coloring mode.
        /// </summary>
        public ColoringMode CurrentColoring
        {
            get { return currentColoring; }
            set
            {
                currentColoring = value;
                PropertyChanged?.Invoke(instance, new PropertyChangedEventArgs(nameof(CurrentColoring)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ElementColoring()
        {
            uiSettings = new UISettings();
            sysAccentColor = (Color)Application.Current.Resources["SystemAccentColorLight2"];
            uiSettings.ColorValuesChanged += uiSettings_ColorValuesChanged;
            currentColoring = ColoringMode.ByCategory;
        }

        private async void uiSettings_ColorValuesChanged(UISettings sender, object args)
        {
            sysAccentColor = sender.GetColorValue(UIColorType.AccentLight2);
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => PropertyChanged?.Invoke(instance, new PropertyChangedEventArgs(nameof(CurrentColoring)))
            );
        }

        public Color GetColorForElement(Element elem)
        {
            switch (currentColoring)
            {
                case ColoringMode.ByCategory:
                    return ColoringTables.ByCategoryTable[(int)elem.Category];
                case ColoringMode.ByBlock:
                    return ColoringTables.ByBlockTable[elem.Block];
                case ColoringMode.ByState:
                    return ColoringTables.ByStateTable[(int)elem.StandardState];
                default:
                    return sysAccentColor;
            }
        }

        public Dictionary<int, Color> GetCurrentColorDictionary()
        {
            switch (currentColoring)
            {
                case ColoringMode.ByCategory:
                    return ColoringTables.ByCategoryTable;
                case ColoringMode.ByBlock:
                    return ColoringTables.ByBlockTable;
                case ColoringMode.ByState:
                    return ColoringTables.ByStateTable;
                default:
                    return null;
            }
        }
    }
}
