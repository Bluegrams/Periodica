using System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Bluegrams.Periodica.Helpers
{
    public class AppTheme
    {
        // Instance
        private static readonly AppTheme instance;

        static AppTheme()
        {
            instance = new AppTheme();
        }

        public static AppTheme Instance { get { return instance; } }

        // Members

        private UISettings settings = new UISettings();
        private bool themeExplicit;
        private ApplicationTheme currentTheme;

        public event EventHandler ThemeChanged;

        public ApplicationTheme CurrentTheme
        {
            get { return currentTheme; }
            private set
            {
                currentTheme = value;
                ThemeChanged?.Invoke(this, new EventArgs());
            }
        }

        public bool IsDark { get { return CurrentTheme == ApplicationTheme.Dark; } }

        private AppTheme()
        {
            currentTheme = Application.Current.RequestedTheme;
            settings.ColorValuesChanged += Settings_ColorValuesChanged;
        }

        private async void Settings_ColorValuesChanged(UISettings sender, object args)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => { if (!themeExplicit) CurrentTheme = Application.Current.RequestedTheme; }
            );
        }

        public void SetTheme(ElementTheme theme)
        {
            themeExplicit = theme != ElementTheme.Default;
            if (theme == ElementTheme.Dark) CurrentTheme = ApplicationTheme.Dark;
            else if (theme == ElementTheme.Light) CurrentTheme = ApplicationTheme.Light;
            else CurrentTheme = Application.Current.RequestedTheme;
            // Set theme for windows root.
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = theme;
            }
        }
    }

    public static class ApplicationThemeExtension
    {
        public static ElementTheme ToElementTheme(this ApplicationTheme theme)
        {
            switch (theme)
            {
                case ApplicationTheme.Light:
                    return ElementTheme.Light;
                default:
                    return ElementTheme.Dark;
            }
        }
    }
}
