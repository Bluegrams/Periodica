using System;
using Windows.UI.Xaml.Controls;
using Bluegrams.Periodica.ViewModels;
using Windows.UI.Xaml;

namespace Bluegrams.Periodica.Controls
{
    public class PeriodicTableControl : UserControl
    {
        public event EventHandler<ElementEventArgs> ElementClicked;

        public static readonly DependencyProperty PeriodicTableProperty =
            DependencyProperty.Register("PeriodicTable", typeof(PeriodicTableViewModel), typeof(PeriodicTableControl),
                new PropertyMetadata(0, OnDependencyPropertyChanged));

        public PeriodicTableViewModel PeriodicTable
        {
            get { return (PeriodicTableViewModel)GetValue(PeriodicTableProperty); }
            set { SetValue(PeriodicTableProperty, value); }
        }

        private static void OnDependencyPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((PeriodicTableControl)o).OnPeriodicTableChanged((PeriodicTableViewModel)e.NewValue);
        }

        protected virtual void OnPeriodicTableChanged(PeriodicTableViewModel periodicTable) { }

        protected virtual void OnElementClicked(ElementEventArgs e)
        {
            ElementClicked?.Invoke(this, e);
        }
    }

    public class ElementEventArgs : EventArgs
    {
        public ElementViewModel Element { get; private set; }

        public ElementEventArgs(ElementViewModel elem)
        {
            Element = elem;
        }
    }
}
