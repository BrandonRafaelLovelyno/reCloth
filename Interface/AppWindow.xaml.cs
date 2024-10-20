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

            string userType = "customer"; // This should be set to the user's type

            if (userType == "customer")
            {
                MainFrame.NavigationService.Navigate(new DashboardCustomer());
            }
            else if (userType == "worker")
            {
                MainFrame.NavigationService.Navigate(new DashboardWorker());
            }

            // Set the window to fullscreen
            this.WindowState = WindowState.Maximized;
        }
    }
}
