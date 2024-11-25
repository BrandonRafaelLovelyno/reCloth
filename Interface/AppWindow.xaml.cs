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
using System.Windows.Shapes;
using System.Xml.Serialization;
using Interface.Helpers;
using Interface.Models;
using Interface.ViewModels;

namespace Interface
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        private AppWindowViewModel _viewModel;

        public AppWindow()
        {
            InitializeComponent();

            _viewModel = new AppWindowViewModel();
            DataContext = _viewModel;

            Application.Current.MainWindow = this;

            string userType = UserSession.Current.Role;

            if (userType == "Customer")
            {
                MainFrame.NavigationService.Navigate(new DashboardCustomer());
            }
            else if (userType == "Tailor" || userType == "Designer")
            {
                MainFrame.NavigationService.Navigate(new DashboardWorker());
            }

            // Set the window to fullscreen
            this.WindowState = WindowState.Maximized;
        }

        private void Marketplace_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Marketplace());
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            string userType = UserSession.Current.Role;   

            if (userType == "Customer")
            {
                MainFrame.NavigationService.Navigate(new DashboardCustomer());
            }
            else if (userType == "Tailor" || userType == "Designer")
            {
                MainFrame.NavigationService.Navigate(new DashboardWorker());
            }
        }

        private void Form_Click(object sender, RoutedEventArgs e)
        {
            string userType = UserSession.Current.Role;

            if (userType == "Customer")
            {
                MainFrame.NavigationService.Navigate(new FormCustomer());
            } else { 
                MessageBox.Show("You cannot access this because you are a Worker", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                UserSession.Current.UserId = null;
                UserSession.Current.Role = null;
                UserSession.Current.Name = null;
                MainWindow mainWindow = new MainWindow();

                Window.GetWindow(this)?.Close();

                mainWindow.Show();

                MessageBox.Show("You are logged out!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
