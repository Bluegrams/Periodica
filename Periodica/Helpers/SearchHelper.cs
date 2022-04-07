using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Bluegrams.Periodica.ViewModels;

namespace Bluegrams.Periodica.Helpers
{
    class SearchHelper : INotifyPropertyChanged
    {
        private PeriodicTableViewModel table;

        public event PropertyChangedEventHandler PropertyChanged;

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            private set
            {
                searchQuery = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchQuery)));
            }
        }

        private bool searchApplied;
        public bool SearchApplied
        {
            get { return searchApplied; }
            private set
            {
                searchApplied = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchApplied)));
            }
        }

        public SearchHelper(PeriodicTableViewModel table)
        {
            this.table = table;
        }

        public IEnumerable<ElementViewModel> SearchFor(string query)
        {
            return table.ElementList.Where(searchQueryFunc(query));
        }

        public void ApplySearch(string query)
        {
            table.ClearAllFilters();
            table.AddFilter("search", searchQueryFunc(query));
            SearchQuery = query;
            SearchApplied = true;
        }

        public void ClearSearch()
        {
            table.RemoveFilter("search");
            SearchQuery = "";
            SearchApplied = false;
        }

        private Func<ElementViewModel, bool> searchQueryFunc(string query)
        {
            return (vm) =>
            {
                return vm.Element.LocalizedName.ToLower().Contains(query.ToLower()) 
                || vm.Element.Symbol.ToLower().Contains(query.ToLower())
                || vm.Element.AtomicNumber.ToString().Equals(query);
            };
        }
    }
}
