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
        private Contract? DesignerContract {  get; set; }

        private Contract? TailorContract { get; set; }

        private string _selectedDesignerStatus;
        public string SelectedDesignerStatus
        {
            get => _selectedDesignerStatus;
            set
            {
                _selectedDesignerStatus = value;
                OnPropertyChanged(nameof(SelectedDesignerStatus));
                FilterOrders();
            }
        }

        private string _selectedTailorStatus;
        public string SelectedTailorStatus
        {
            get => _selectedTailorStatus;
            set
            {
                _selectedTailorStatus = value;
                OnPropertyChanged(nameof(SelectedTailorStatus));
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
            FilteredOrders.Clear();

            var filtered = Orders;

            // Apply designer status filter if applicable
            if (!string.IsNullOrWhiteSpace(SelectedDesignerStatus))
            {
                filtered = new ObservableCollection<Order>(
                    filtered.Where(o => o.DesignerStatus == SelectedDesignerStatus)
                );
            }

            // Apply tailor status filter if applicable
            if (!string.IsNullOrWhiteSpace(SelectedTailorStatus))
            {
                filtered = new ObservableCollection<Order>(
                    filtered.Where(o => o.TailorStatus == SelectedTailorStatus)
                );
            }

            foreach (var order in filtered)
            {
                FilteredOrders.Add(order);
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
