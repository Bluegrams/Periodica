using System;
using Bluegrams.Periodica.Data;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Bluegrams.Periodica.Helpers;
using Windows.UI.Xaml.Media;

namespace Bluegrams.Periodica.ViewModels
{
    public static class TablePositionConverter
    {
        public static int Convert(Element element, string parameter)
        {
            bool isGroup = parameter == "Group";
            if (element.Category == ElementCategory.Lanthanoid)
            {
                return isGroup ? element.AtomicNumber - 53 : 9;
            }
            else if (element.Category ==  ElementCategory.Actinoid)
            {
                return isGroup ? element.AtomicNumber - 85 : 10;
            }
            else return isGroup ? (int)element.Group : element.Period;
        }
    }

    public class PropertyUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || parameter == null) return value;
            return ElementManipulation.GetPropertyValue((Element)value, (string)parameter, true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorThemeAdapter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var col = ((SolidColorBrush)value).Color;
            var factor = AppTheme.Instance.IsDark ? 0.65 : 1;
            return new SolidColorBrush(Color.FromArgb(col.A, (byte)(col.R * factor), (byte)(col.G * factor), (byte)(col.B * factor)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TemperatureUnitToBoolConverter : EnumToBoolConverter
    {
        public TemperatureUnitToBoolConverter() : base()
        {
            EnumType = typeof(TemperatureUnit);
        }
    }
}
