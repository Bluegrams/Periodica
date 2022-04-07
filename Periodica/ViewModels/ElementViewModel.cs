using System;
using System.ComponentModel;
using Bluegrams.Periodica.Data;
using Bluegrams.Periodica.Helpers;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;

namespace Bluegrams.Periodica.ViewModels
{
    public class ElementViewModel : INotifyPropertyChanged
    {
        public Element Element { get; private set; }
        public PeriodicTableViewModel PeriodicTable { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Brush colorBrush;
        public Brush ColorBrush
        {
            get { return colorBrush; }
            private set
            {
                colorBrush = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorBrush)));
            }
        }

        public Brush ImagedColorBrush { get; private set; }

        public string LocalizedCategoryName { get; private set; }

        public ImageResource[] Images { get { return ImageResourceTables.Images[Element.AtomicNumber - 1]; } }

        private bool visible = true;
        public bool Visible
        {
            get { return visible; }
            set { visible = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visible))); }
        }

        public ElementViewModel(Element element, PeriodicTableViewModel tableViewModel)
        {
            Element = element;
            PeriodicTable = tableViewModel;
            Coloring_PropertyChanged(null, null);
            var loader = ResourceLoader.GetForViewIndependentUse("CodeStrings");
            LocalizedCategoryName = loader.GetString("Title_ByCategory" + (int)element.Category);
            ElementColoring.Instance.PropertyChanged += Coloring_PropertyChanged;
            AppTheme.Instance.ThemeChanged += Instance_ThemeChanged;
            Settings.Instance.PropertyChanged += SettingsInstance_PropertyChanged;
        }

        private void Instance_ThemeChanged(object sender, EventArgs e)
        {
            ColorBrush = new SolidColorBrush(getAdjustedElementColor());
            Coloring_PropertyChanged(null, null);
        }

        // Notify all objects with bindings to this element that settings have changed.
        private void SettingsInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Element)));
#if DEBUG
            System.Diagnostics.Debug.WriteLine(
                String.Format("ElementViewModel.cs: Settings have changed ({0}).", e.PropertyName)
                );
#endif
        }

        public static explicit operator Element(ElementViewModel viewModel) { return viewModel.Element; }

        private void Coloring_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ColorBrush = new SolidColorBrush(getAdjustedElementColor());
            if (ElementColoring.Instance.CurrentColoring == ColoringMode.WithImage
                && Images.Length > 0)
            {
                BitmapImage img = new BitmapImage();
                img.UriSource = Images[0].Uri;
                img.DecodePixelType = DecodePixelType.Logical;
                img.DecodePixelHeight = 74;
                ImageBrush brush = new ImageBrush();
                ImagedColorBrush = brush;
                brush.ImageSource = img;
                brush.Opacity = 0.6;
                brush.Stretch = Stretch.UniformToFill;
            }
            else ImagedColorBrush = ColorBrush;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImagedColorBrush)));
        }

        private Color getAdjustedElementColor()
        {
            double factor = AppTheme.Instance.IsDark ? 0.65 : 1;
            Color eCol = ElementColoring.Instance.GetColorForElement(Element);
            return Color.FromArgb(255, (byte)(factor * eCol.R), 
                (byte)(factor * eCol.G), (byte)(factor * eCol.B));
        }

        public object GetListPropertyValue(int number)
        {
            return ElementManipulation.GetPropertyValue(this.Element, "list:" + number);
        } 
    }
}
