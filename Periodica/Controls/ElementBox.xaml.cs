using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Bluegrams.Periodica.ViewModels;
using Bluegrams.Periodica.Helpers;
using System.ComponentModel;

namespace Bluegrams.Periodica.Controls
{
    public sealed partial class ElementBox : UserControl, INotifyPropertyChanged
    {
        public event RoutedEventHandler Click;
        public event PropertyChangedEventHandler PropertyChanged;

        public ElementViewModel Element { get; set; }
        private object DisplayedPropertyValue { get; set; }

        public void SetDisplayedProperty(string property)
        {
            DisplayedPropertyValue = ElementManipulation.GetPropertyValue(Element.Element, property);
            if (DisplayedPropertyValue != null && DisplayedPropertyValue is double?)
                DisplayedPropertyValue = Math.Round((double)DisplayedPropertyValue, 4).ToString();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayedPropertyValue)));
        }

        public ElementBox()
        {
            this.InitializeComponent();
            RenderPeriodicHelper.Instance.PropertyChanged += RenderPeriodic_PropertyChanged;
        }

        private void RenderPeriodic_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (RenderPeriodicHelper.Instance.RenderMode)
                blockProp.Visibility = Visibility.Visible;
            else blockProp.Visibility = ((Frame)Window.Current.Content).ActualHeight > 540 
                    ? Visibility.Visible 
                    : Visibility.Collapsed;
        }

        public void SetEnabled(Boolean isEnabled)
        {
            this.IsEnabled = isEnabled;
            panMain.Opacity = isEnabled ? 1 : 0.2;
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            Click?.Invoke(this, e);
            base.OnPointerPressed(e);
        }

        private void userControl_GotFocus(object sender, RoutedEventArgs e)
        {
            panMain.BorderBrush = new SolidColorBrush(AppTheme.Instance.IsDark ? Colors.White : Colors.Black);

        }

        private void userControl_LostFocus(object sender, RoutedEventArgs e)
        {
            panMain.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private void panMain_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            this.Focus(FocusState.Programmatic);
        }
    }
}
