using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.Globalization;
using Windows.UI.Xaml;
using System.Linq;

namespace Bluegrams.Periodica.Helpers
{
    public class AppLanguage : INotifyPropertyChanged
    {
        // Instance
        private static readonly AppLanguage instance;

        static AppLanguage()
        {
            instance = new AppLanguage();
        }

        public static AppLanguage Instance { get { return instance; } }

        // Members
        public List<Language> AppLanguageOptions { get; private set; }

        private bool useSystemPreference;
        public bool UseSystemPreference
        {
            get { return useSystemPreference; }
            set
            {
                if (value)
                {
                    ApplicationLanguages.PrimaryLanguageOverride = "";
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedOption)));
                }
                else SelectedOption = 0;
                useSystemPreference = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UseSystemPreference)));
            }
        }

        private string selectedLanguage { get { return ApplicationLanguages.PrimaryLanguageOverride; } }
        public int SelectedOption
        {
            get { return AppLanguageOptions.FindIndex(v => v.LanguageTag == selectedLanguage); }
            set
            {
                ApplicationLanguages.PrimaryLanguageOverride = AppLanguageOptions[value].LanguageTag;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedOption)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
   

        private AppLanguage()
        {
            AppLanguageOptions = new List<Language>();
            AppLanguageOptions.AddRange(ApplicationLanguages.ManifestLanguages
                                        .Select((tag) => new Language(tag))
                                        .OrderBy((v) => v.NativeName));
            useSystemPreference = String.IsNullOrEmpty(ApplicationLanguages.PrimaryLanguageOverride);
        }
    }
}
