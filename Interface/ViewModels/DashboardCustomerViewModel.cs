﻿using System;
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
        private readonly DatabaseHelper dbHelper;

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

        public DashboardCustomerViewModel()
        {
            CustomerName = $"Hello, {UserSession.Current.Name}!";
            dbHelper = new DatabaseHelper();
            Orders = new ObservableCollection<Order>();

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
                Orders.Add(new Order(orderId));
            }

            TotalOrders = Orders.Count;
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