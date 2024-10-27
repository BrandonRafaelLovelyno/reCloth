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

        public DashboardCustomer(string customer_id)
        {
            _customer = new Customer(customer_id);
            InitializeComponent();
            fetchOrder();
        }

        public void fetchOrder()
        {
            string query = "SELECT * FROM orders";

            // Execute the query and get the reader
            using (NpgsqlDataReader res = dbHelper.executeQuery(query))
            {
                if (res != null)
                {
                    // Read the results
                    while (res.Read())
                    {
                        string title = res["title"].ToString();
                        double budgetValue = Convert.ToDouble(res["budget"]);
                        string budget = $"Rp {budgetValue}";
                        string status = res["is_done"].ToString() == "True" ? "Finished" : "Ongoing";

                        Console.WriteLine($"Title: {title}, Budget: {budget}, Status: {status}");

                        orders.Add(new Order { Title = title, Budget = budget, Status = status });
                    }
                }
                else
                {
                    Console.WriteLine("No results returned.");
                }
            } // The reader is automatically closed here
            OrderList.ItemsSource = orders;
        }
    }
}
