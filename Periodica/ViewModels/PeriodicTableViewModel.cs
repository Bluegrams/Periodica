using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bluegrams.Periodica.Data;

namespace Bluegrams.Periodica.ViewModels
{
    public class PeriodicTableViewModel : IEnumerable
    {
        private PeriodicTable table;

        public ObservableCollection<ElementViewModel> ElementList { get; private set; }

        public ObservableCollection<ElementViewModel> FilteredElementList { get; private set; }

        private Dictionary<string, Func<ElementViewModel, bool>> filters;

        public PeriodicTableViewModel()
        {
            table = PeriodicTable.Load(System.Globalization.CultureInfo.CurrentUICulture);
            ElementList = new ObservableCollection<ElementViewModel>();
            ElementList.CollectionChanged += (o, e) => updateFilteredList();
            FilteredElementList = new ObservableCollection<ElementViewModel>();
            filters = new Dictionary<string, Func<ElementViewModel, bool>>();
            for (int i = 1; i <= table.Elements.Count; i++)
            {
                ElementList.Add(new ElementViewModel(table[i], this));
            }
        }

        public ElementViewModel GetNextElement(int atomicNumber)
        {
            int next = atomicNumber == 118 ? 1 : atomicNumber + 1;
            return ElementList.Where(v => v.Element.AtomicNumber == next).FirstOrDefault();
        }

        public ElementViewModel GetPreviousElement(int atomicNumber)
        {
            int prev = atomicNumber == 1 ? 118 : atomicNumber - 1;
            return ElementList.Where(v => v.Element.AtomicNumber == prev).FirstOrDefault();
        }

        public void AddFilter(string key, Func<ElementViewModel, bool> filter)
        {
            filters.Add(key, filter);
            applyFilters();
        }

        public void RemoveFilter(string key)
        {
            filters.Remove(key);
            applyFilters();
        }

        public void ClearAllFilters()
        {
            filters.Clear();
            applyFilters();
        }

        public void Sort(Func<ElementViewModel, object> sorting, bool descend)
        {
            var ordered = descend ? ElementList.OrderByDescending(sorting) : ElementList.OrderBy(sorting);
            ElementList = new ObservableCollection<ElementViewModel>(ordered);
            updateFilteredList();
        }

        private void applyFilters()
        {
            Func<ElementViewModel, bool> allFilters = (args) => filters.Count == 0;
            foreach (var filter in filters.Values)
                allFilters = allFilters.Or(filter);
            for (int i = 0; i < ElementList.Count; i++)
            {
                ElementList[i].Visible = allFilters.Invoke(ElementList[i]);
            }
            updateFilteredList();
        }

        private void updateFilteredList()
        {
            FilteredElementList.Clear();
            foreach (var elem in ElementList.Where((el) => el.Visible))
                FilteredElementList.Add(elem);
        }

        public IEnumerator GetEnumerator()
        {
            return ElementList.GetEnumerator();
        }
    }

    public static class FuncExtensions
    {
        public static Func<T, bool> Or<T>(this Func<T, bool> f1, Func<T, bool> f2)
        {
            return arg => f1(arg) || f2(arg);
        }
    }
}
