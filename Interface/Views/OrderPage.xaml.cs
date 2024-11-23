﻿using CloudinaryDotNet.Actions;
using Interface.ViewModels;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interface
{
    public partial class OrderPage : System.Windows.Controls.Page
    {
        private OrderPageViewModel _viewModel;
        public string Id {  get; private set; }
        public string _orderId { get; private set; }
        
        public OrderPage(string orderId)
        {
            InitializeComponent();
            _viewModel = new OrderPageViewModel(orderId);
            DataContext = _viewModel;
            _orderId = orderId;
        }

        private void Route_to_Contract(object sender, MouseButtonEventArgs e)
        {
            // MessageBox.Show("Feature is Under Construction", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            var appWindow = Application.Current.MainWindow as AppWindow;
            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new ProposalPage(_orderId));
            }
        }

        private void Route_to_Form_Worker(object sender, MouseButtonEventArgs e)
        {
            // MessageBox.Show("Feature is Under Construction", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            var appWindow = Application.Current.MainWindow as AppWindow;
            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new FormWorker(_orderId));
            }
        }
    }
}
