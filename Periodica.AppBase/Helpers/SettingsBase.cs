using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Windows.Storage;
using Windows.UI.Xaml;
using Newtonsoft.Json;

namespace Bluegrams.Periodica.Helpers
{
    public abstract class SettingsBase : INotifyPropertyChanged
    {

        /// <summary>
        /// Wraps the app theme for the settings.
        /// </summary>
        [Setting]
        public ElementTheme SelectedTheme
        {
            get { return AppTheme.Instance.CurrentTheme.ToElementTheme(); }
            set
            {
                AppTheme.Instance.SetTheme(value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnSettingChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Load the settings properties from storage.
        /// </summary>
        public void LoadSettings()
        {
            var properties = GetType().GetProperties().Where(prop => prop.GetCustomAttribute(typeof(SettingAttribute)) != null);
            foreach (var prop in properties)
            {
                var value = ApplicationData.Current.LocalSettings.Values[prop.Name];
                if (value != null)
                {
                    try
                    {
                        Type type = prop.PropertyType;
                        prop.SetValue(this, JsonConvert.DeserializeObject((string)value, type));
                    }
                    // Overwrite the current value if loading failed.
                    catch (Exception)
                    {
                        ApplicationData.Current.LocalSettings.Values[prop.Name] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Save the settings properties to file before the app shuts down.
        /// </summary>
        public void SaveSettings()
        {
            var properties = GetType().GetProperties().Where(prop => prop.GetCustomAttribute(typeof(SettingAttribute)) != null);
            foreach (var prop in properties)
            {
                var serialized = JsonConvert.SerializeObject(prop.GetValue(this));
                ApplicationData.Current.LocalSettings.Values[prop.Name] = serialized;
            }
        }
    }

    public class SettingAttribute : Attribute
    { }
}
