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
using reCloth;

namespace Interface
{
    /// <summary>
    /// Interaction logic for DashboardCustomer.xaml
    /// </summary>
    public partial class DashboardCustomer : Page
    {
        public DashboardCustomer()
        {
            InitializeComponent();

            // Create sample data
            List<Order> orders = new List<Order>
            {
                new Order { Title = "Project A", Budget = "$1000", Status = "Posted" },
                new Order { Title = "Project B", Budget = "$2000", Status = "Proposed" },
                new Order { Title = "Project C", Budget = "$1500", Status = "On Going" },
                new Order { Title = "Project D", Budget = "$3000", Status = "Finished" },
                new Order { Title = "Project D", Budget = "$3000", Status = "Finished" }
            };

            OrderList.ItemsSource = orders;
        }
    }
}
