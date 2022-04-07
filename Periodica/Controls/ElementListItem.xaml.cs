using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Bluegrams.Periodica.Helpers;

namespace Bluegrams.Periodica.Controls
{
    public sealed partial class ElementListItem : UserControl
    {
        public ElementListItem()
        {
            this.InitializeComponent();
        }

        private void panMain_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            panMain.BorderBrush = new SolidColorBrush(AppTheme.Instance.IsDark ? Colors.White : Colors.Black);
        }

        private void panMain_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            panMain.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }
    }

}
