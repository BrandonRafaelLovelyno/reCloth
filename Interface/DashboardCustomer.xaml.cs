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
    /// Interaction logic for DashboardCustomer.xaml
    /// </summary>
    public partial class DashboardCustomer : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private Customer _customer;
        private List<Order> orders = new List<Order>();

        public DashboardCustomer()
        {
            _customer = new Customer(UserSession.Current.UserId);
            InitializeComponent();
            fetchOrder();
        }

        public void fetchOrder()
        {
            string query = "SELECT * FROM orders";

            var rows = dbHelper.executeGetQuery(query,"id_order");

            foreach (var row in rows)
            {
                string orderId = dbHelper.convertObject<Guid>(row["id_order"]).ToString();
                orders.Add(new Order(orderId));
            }
            
            OrderList.ItemsSource = orders;
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

        private void Route_to_Form(object sender, MouseButtonEventArgs e)
        {
            var appWindow = Application.Current.MainWindow as AppWindow;

            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new FormCustomer());
            }
        }
    }
}
