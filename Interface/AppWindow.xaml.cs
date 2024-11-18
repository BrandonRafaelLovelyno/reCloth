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
using Interface.Models;

namespace Interface
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow = this;

            MainFrame.NavigationService.Navigate(new Marketplace());

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
            MainFrame.NavigationService.Navigate(new FormCustomer());
        }
    }
}
