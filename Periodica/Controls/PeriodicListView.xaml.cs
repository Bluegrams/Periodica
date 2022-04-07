using System;
using Windows.UI.Xaml.Controls;
using Bluegrams.Periodica.ViewModels;
using Windows.UI.Xaml.Controls.Primitives;
using System.Collections.ObjectModel;
using Bluegrams.Periodica.Helpers;

namespace Bluegrams.Periodica.Controls
{
    public sealed partial class PeriodicListView : PeriodicTableControl
    {
        private ObservableCollection<bool?> SortingStates = new ObservableCollection<bool?> { false, null, null, null, null, null, null };
        // TODO really ugly
        private Func<ElementViewModel, object>[] elementProperties = new Func<ElementViewModel, object>[]
        {
            v => v.Element.AtomicNumber,
            v => v.Element.Symbol,
            v => v.Element.LocalizedName,
            v => v.GetListPropertyValue(3),
            v => v.GetListPropertyValue(4),
            v => v.GetListPropertyValue(5),
            v => v.GetListPropertyValue(6)
        };

        public PeriodicListView()
        {
            this.InitializeComponent();
            Settings.Instance.PropertyChanged += SettingsInstance_PropertyChanged;
            SettingsInstance_PropertyChanged(null, null);
        }

        private void SettingsInstance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            tog0.Content = ElementManipulation.GetListPropertyString(3);
            tog1.Content = ElementManipulation.GetListPropertyString(4);
            tog2.Content = ElementManipulation.GetListPropertyString(5);
            tog3.Content = ElementManipulation.GetListPropertyString(6);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ElementViewModel elem = (ElementViewModel)e.ClickedItem;
            OnElementClicked(new ElementEventArgs(elem));
        }

        private void ToggleButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int clickedIndex = int.Parse((string)((ToggleButton)sender).Tag);
            bool isChecked = (bool)((ToggleButton)sender).IsChecked;
            for (int i = 0; i < SortingStates.Count; i++)
                SortingStates[i] = null;
            ((ToggleButton)sender).IsChecked = isChecked;
            PeriodicTable.Sort(elementProperties[clickedIndex], isChecked);
        }
    }
}
