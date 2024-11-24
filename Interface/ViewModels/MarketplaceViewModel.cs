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
                order.DesignerStatus = GetDesignerStatus(order);
                order.TailorStatus = GetTailorStatus(order);
                Orders.Add(order);
            }
            FilterOrders();
        }

        public string GetDesignerStatus(Order order)
        {
            if (order.findProposedContract("Designer") == null)
                return "Needs Designer";

            if (order.findAcceptedContract("Designer") == null)
                return "Proposed by Designer";

            if (order.findFinishedContract("Designer") == null)
                return "On Progress by Designer";

            return "Finished by Designer";
        }

        public string GetTailorStatus(Order order)
        {
            if (order.findProposedContract("Tailor") == null)
                return "Needs Tailor";

            if (order.findAcceptedContract("Tailor") == null)
                return "Proposed by Tailor";

            if (order.findFinishedContract("Tailor") == null)
                return "On Progress by Tailor";

            return "Finished by Tailor";
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
