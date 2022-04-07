using System;
using System.Reflection;
using Bluegrams.Periodica.Data;
using Bluegrams.Periodica.Helpers;
using Windows.ApplicationModel.Resources;

namespace Bluegrams.Periodica.ViewModels
{
    static class ElementManipulation
    {
        private static ResourceLoader loader;

        static ElementManipulation()
        {
            loader = ResourceLoader.GetForViewIndependentUse();
        }

        // Property Descriptor format:
        // The strings have the following format: 'tag:name', where 'tag' describes some additional information
        // (e.g. formatting) and 'name' refers to the name of the property.

        /// <summary>
        /// Looks up the (translated) description string for the property displayed at the given index.
        /// </summary>
        /// <param name="index">The index of the property for which to look up the description.</param>
        /// <returns>The description resource for the property currently displayed at index.</returns>
        public static string GetListPropertyString(int index)
        {
            string[] split = Settings.Instance.ListProperties[index].Split(':');
            string propName = split[split.Length - 1];
            return loader.GetString("settingsProp" + propName + "/Content");
        }

        /// <summary>
        /// Retrieves the value of a given property descriptor for a given element.
        /// </summary>
        /// <param name="elem">The element for which to retrieve the value.</param>
        /// <param name="propertyName">A property descriptor of the form 'tag:name'.</param>
        /// <param name="withUnit">true if the value should be returned formatted with its unit.</param>
        /// <returns></returns>
        public static object GetPropertyValue(Element elem, string propertyName, bool withUnit = false)
        {
            string[] split = propertyName.Split(':');
            // look up property names of type 'list:<number>' in the current settings to get the 
            // actual name of the property that is currently displayed there. 
            if (split[0] == "list")
            {
                split = Settings.Instance.ListProperties[int.Parse(split[1])].Split(':');
            }
            int last = split.Length - 1;
            var value = elem.GetType().GetProperty(split[last]).GetValue(elem);
            bool foundAttribute = false;
            for (int i = 0; i < last; i++)
            {
                switch (split[i])
                {
                    case "temp":
                        value = ConvertedTemperature((double?)value, out string unit);
                        if (withUnit) value += " " + unit;
                        foundAttribute = true;
                        break;
                }
            }
            if (!foundAttribute && value != null && withUnit)
            {
                var property = typeof(Element).GetProperty(split[last]);
                if (property != null)
                {
                    object attribute = property.GetCustomAttribute(typeof(UnitAttribute));
                    if (attribute != null)
                    {
                        string unit = ((UnitAttribute)attribute).UnitString;
                        value += " " + unit;
                    }
                }
            }
            return value;
        }

        public static double? ConvertedTemperature(double? temp, out string unit)
        {
            unit = "";
            if (temp == null) return null;
            switch (Settings.Instance.TemperatureUnit)
            {
                case TemperatureUnit.Celsius:
                    unit = "°C";
                    return Math.Round((double)temp - 273.15);
                case TemperatureUnit.Fahrenheit:
                    unit = "°F";
                    return Math.Round((double)temp * 9 / 5 - 459.67, 3);
                default:
                    unit = "K";
                    return temp;
            }
        }
    }
}
