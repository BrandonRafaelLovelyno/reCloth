using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Execute the query and get the reader
            using (NpgsqlDataReader res = dbHelper.executeQuery(query))
            {
                if (res != null)
                {
                    // Read the results
                    while (res.Read())
                    {
                        string title = res["title"].ToString();
                        double budgetValue = Convert.ToDouble(res["budget"]);
                        string budget = $"Rp {budgetValue}"; // Assuming you want a formatted currency string
                        string status = res["is_done"].ToString() == "True" ? "Finished" : "Ongoing";

                        // Add to the Orders collection
                        Orders.Add(new Order { Title = title, Budget = budget, Status = status });
                    }

                    // Initialize FilteredOrders with all fetched Orders
                    foreach (var order in Orders)
                    {
                        FilteredOrders.Add(order);
                    }
                }
                else
                {
                    Console.WriteLine("No results returned.");
                }
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
