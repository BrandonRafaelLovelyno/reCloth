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
    /// Interaction logic for DashboardWorker.xaml
    /// </summary>
    public partial class DashboardWorker : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public DashboardWorker()
        {
            InitializeComponent();

            // Create sample data
            List<Order> orders = new List<Order>
            {
                new Order { Title = "Project A", Budget = "$1000", Status = "Submitted" },
                new Order { Title = "Project B", Budget = "$2000", Status = "On Going" },
                new Order { Title = "Project C", Budget = "$1500", Status = "On Going" },
                new Order { Title = "Project D", Budget = "$3000", Status = "Finished" },
                new Order { Title = "Project D", Budget = "$3000", Status = "Finished" }
            };

            ContractList.ItemsSource = orders;
        }

        public fetchOrder
    }
}
