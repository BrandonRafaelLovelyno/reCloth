using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Interface.ViewModels
{
    public class MarketplaceViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Order> Orders { get; set; } // Original list of orders
        public ObservableCollection<Order> FilteredOrders { get; set; } // Filtered list of orders

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterOrders();
            }
        }

        public ICommand SearchCommand { get; }

        private readonly DatabaseHelper dbHelper;

        public MarketplaceViewModel()
        {
            Orders = new ObservableCollection<Order>();
            FilteredOrders = new ObservableCollection<Order>();

            SearchCommand = new RelayCommand(FilterOrders);

            dbHelper = new DatabaseHelper();

            fetchOrder();
        }

        private void fetchOrder()
        {
            string query = "SELECT * FROM orders";

            var rows = dbHelper.executeGetQuery(query, "id_order");

            foreach (var row in rows)
            {
                string orderId = dbHelper.convertObject<Guid>(row["id_order"]).ToString();

                FilteredOrders.Add(new Order(orderId));
            }
        }

        private void FilterOrders()
        {
            FilteredOrders.Clear();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                foreach (var order in Orders)
                {
                    FilteredOrders.Add(order);
                }
            }
            else
            {
                var filtered = Orders
                    .Where(o => o.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
                
                foreach (var order in filtered)
                {
                    FilteredOrders.Add(order);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged (string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
