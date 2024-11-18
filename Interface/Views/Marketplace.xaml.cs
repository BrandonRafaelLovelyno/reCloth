using Interface.Helpers;
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
    /// <summary>
    /// Interaction logic for Marketplace.xaml
    /// </summary>
    public partial class Marketplace : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public Marketplace()
        {
            InitializeComponent();
        }

        private void Route_to_OrderPage(object sender, MouseButtonEventArgs e)
        {
            var appWindow = Application.Current.MainWindow as AppWindow;
            Console.WriteLine("Try to navigate to OrderPage");
            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new OrderPage("80e47bb4-9ea3-11ef-8ca2-1e901716c947"));
                Console.WriteLine("Navigating to OrderPage");
            }
        }
    }
}
