using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Interface.Models;
using Interface.Helpers;

namespace Interface.ViewModels
{
    internal class DashboardCustomerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Order> FilteredOrders { get; set; }
        private readonly DatabaseHelper dbHelper;

        public ICommand SearchCommand { get; }
        public ICommand NavigateToOrderCommand { get; }
        public ICommand RouteToFormCommand { get; }

        private int totalOrders;
        public int TotalOrders
        {
            get => totalOrders;
            set
            {
                totalOrders = value;
                OnPropertyChanged(nameof(TotalOrders));
            }
        }

        private string customerName;
        public string CustomerName
        {
            get => customerName;
            set
            {
                customerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }

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

        private string _selectedOrderStatus;
        public string SelectedOrderStatus
        {
            get => _selectedOrderStatus;
            set
            {
                _selectedOrderStatus = value;
                OnPropertyChanged(nameof(SelectedOrderStatus));
                FilterOrders();
            }
        }

        public DashboardCustomerViewModel()
        {
            CustomerName = $"Hello, {UserSession.Current.Name}!";
            dbHelper = new DatabaseHelper();
            Orders = new ObservableCollection<Order>();

            FilteredOrders = new ObservableCollection<Order>();

            SearchCommand = new RelayCommand<object>(_ => FilterOrders());

            // Initialize commands
            NavigateToOrderCommand = new RelayCommand<string>(NavigateToOrder);
            RouteToFormCommand = new RelayCommand<object>(_ => RouteToForm());

            FetchOrders();
        }

        private string fetchCustomerId()
        {
            string query = $"SELECT * from customers where id_user = '{UserSession.Current.UserId}';";
            
            var rows = dbHelper.executeGetQuery(query, "id_customer");

            string customerId = dbHelper.convertObject<Guid>(rows[0]["id_customer"]).ToString();

            return customerId;
        }

        private void FetchOrders()
        {
            string customerId = fetchCustomerId();

            string query = $"SELECT * FROM orders WHERE id_customer = '{customerId}';";

            var rows = dbHelper.executeGetQuery(query, "id_order");

            foreach (var row in rows)
            {
                string orderId = dbHelper.convertObject<Guid>(row["id_order"]).ToString();
                Order order = new Order(orderId);
                order.DesignerStatus = GetDesignerStatus(order);
                order.TailorStatus = GetTailorStatus(order);
                Orders.Add(order);
            }
            TotalOrders = Orders.Count;
            FilterOrders();
        }

        public string GetDesignerStatus(Order order)
        {
            bool isAccepted = order.findAcceptedContract("Designer") != null;
            bool isFinished = order.findFinishedContract("Designer") != null;
            bool isProposed = order.findProposedContract("Designer") != null;

            bool isNeed = !isAccepted && !isFinished && !isProposed;

            string status = "";


            if (isNeed)
                status = "Needs Designer";

            if (isProposed)
                status = "Proposed by Designer";

            if (isAccepted)
                status = "On Progress by Designer";

            if (isFinished)
                status = "Finished";

            return status;
        }

        public string GetTailorStatus(Order order)
        {
            bool isAccepted = order.findAcceptedContract("Tailor") != null;
            bool isFinished = order.findFinishedContract("Tailor") != null;
            bool isProposed = order.findProposedContract("Tailor") != null;

            bool isNeed = !isAccepted && !isFinished && !isProposed;

            string status = "";


            if (isNeed)
                status = "Needs Tailor";

            if (isProposed)
                status = "Proposed by Tailor";

            if (isAccepted)
                status = "On Progress by Tailor";

            if (isFinished)
                status = "Finished";

            return status;
        }

        private void FilterOrders()
        {
            // Clear the existing items in FilteredOrders
            FilteredOrders.Clear();

            IEnumerable<Order> filtered = Orders;

            // Apply status filter if applicable
            if (!string.IsNullOrWhiteSpace(SelectedOrderStatus))
            {
                filtered = filtered.Where(o => o.TailorStatus == SelectedOrderStatus);
                filtered = filtered.Where(o => o.DesignerStatus == SelectedOrderStatus);
            }

            // Add the filtered items to FilteredOrders to notify the UI
            foreach (var order in filtered)
            {
                FilteredOrders.Add(order);
            }

            Console.WriteLine(SelectedOrderStatus);
        }


        private void NavigateToOrder(string orderId)
        {
            var appWindow = Application.Current.MainWindow as AppWindow;
            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new OrderPage(orderId));
            }
        }

        private void RouteToForm()
        {
            var appWindow = Application.Current.MainWindow as AppWindow;
            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new FormCustomer());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
