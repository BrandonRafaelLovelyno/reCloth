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
            string userType = "customer"; // This should be set to the user's type

            if (userType == "customer")
            {
                MainFrame.NavigationService.Navigate(new DashboardCustomer("ef49a2d2-943f-11ef-9570-1e901716c947"));
            }
            else if (userType == "worker")
            {
                MainFrame.NavigationService.Navigate(new DashboardWorker("c005666e-943f-11ef-9570-1e901716c947", "Tailor"));
            }
        }
    }
}
