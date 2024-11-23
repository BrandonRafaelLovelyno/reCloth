using Interface.Helpers;
using Interface.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Interface.ViewModels
{
    public class MarketplaceViewModel : INotifyPropertyChanged
    {
        public ICommand NavigateToOrderCommand { get; }
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

            SearchCommand = new RelayCommand<object>(_ => FilterOrders());
            NavigateToOrderCommand = new RelayCommand<string>(NavigateToOrder);

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
                Order order = new Order(orderId);
                order.Status = GetOrderStatus(order);
                Orders.Add(order);
            }
            FilterOrders();
        }

        public string GetOrderStatus(Order order)
        {
            var statusChecks = new (string Role, string StatusIfProposed, string StatusIfAccepted)[]
            {
                ("Designer", "Proposed by Designer", "On Progress by Designer"),
                ("Taylor", "Proposed by Tailor", "On Progress by Tailor")
            };

            foreach (var check in statusChecks)
            {
                string proposedContract = order.findProposedContract(check.Role);
                if (proposedContract == null)
                    return $"Needs {check.Role}";

                string acceptedContract = order.findAcceptedContract(check.Role);
                if (acceptedContract == null)
                    return check.StatusIfProposed;

                string finishedContract = order.findFinishedContract(check.Role);
                if (finishedContract == null)
                    return check.StatusIfAccepted;
            }

            return "Finished";
        }

        private void FilterOrders()
        {
            Console.WriteLine(SearchText);
            FilteredOrders.Clear();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Console.WriteLine(Orders[0]);
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

        private void NavigateToOrder(string orderId)
        {
            var appWindow = Application.Current.MainWindow as AppWindow;
            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new OrderPage(orderId));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
