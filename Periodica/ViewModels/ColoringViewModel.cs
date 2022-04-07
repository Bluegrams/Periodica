using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Bluegrams.Periodica.Data;
using Windows.ApplicationModel.Resources;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Bluegrams.Periodica.ViewModels
{
    class ColoringViewModel
    {
        private ElementColoring coloring;
        private ResourceLoader loader;

        private ObservableCollection<string> coloringOptions;
        public ObservableCollection<string> ColoringOptions { get { return coloringOptions; } }

        // Static version is e.g. used for bitmap export.
        public static ObservableCollection<ColoringOption> StaticExplanationItems { get; private set; }
        public ObservableCollection<ColoringOption> ExplanationItems { get { return StaticExplanationItems; } }

        public event EventHandler PreviewOptionChanged;

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                coloring.CurrentColoring = (ColoringMode)value;
                PreviewOptionChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PreviewOptionChanged)));
                setExplanationItems();
            }
        }

        public ColoringViewModel()
        {
            coloring = ElementColoring.Instance;
            loader = ResourceLoader.GetForViewIndependentUse("CodeStrings");
            coloringOptions = new ObservableCollection<string>();
            foreach (string option in Enum.GetNames(typeof(ColoringMode)))
            {
                coloringOptions.Add(loader.GetString("Color_" + option));
            }
            selectedIndex = (int)coloring.CurrentColoring;
            StaticExplanationItems = new ObservableCollection<ColoringOption>();
            setExplanationItems();
        }

        private void setExplanationItems()
        {
            ExplanationItems.Clear();
            string coloringName = coloring.CurrentColoring.ToString();
            var categoryDict = coloring.GetCurrentColorDictionary();
            if (categoryDict == null) return;
            foreach (var colorPair in categoryDict)
            {
                string text = loader.GetString("Title_" + coloringName + colorPair.Key);
                ExplanationItems.Add(new ColoringOption(text, new SolidColorBrush(colorPair.Value)));
            }
        }
    }

    class ColoringOption : INotifyPropertyChanged
    {
        public string Name { get; private set; }

        public SolidColorBrush Brush { get; private set; }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected))); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ColoringOption(string name, SolidColorBrush brush)
        {
            Name = name;
            Brush = brush;
        }
    }
}
