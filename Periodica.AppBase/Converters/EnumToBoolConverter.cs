using System;
using Windows.UI.Xaml.Data;

namespace Bluegrams.Periodica.ViewModels
{
    public class EnumToBoolConverter : IValueConverter
    {
        public Type EnumType { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter is string enumExpected)
            {
                if (Enum.IsDefined(EnumType, value))
                {
                    return Enum.Parse(EnumType, enumExpected).Equals(value);
                }
            }
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
