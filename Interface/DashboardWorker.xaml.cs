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
using static System.Net.Mime.MediaTypeNames;

namespace Interface
{
    /// <summary>
    /// Interaction logic for DashboardWorker.xaml
    /// </summary>
    public partial class DashboardWorker : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private Worker _worker;
        private List<Order> _orders;
        public DashboardWorker(string id_worker, string role)
        {
            this._worker = new Worker(id_worker, role);
            InitializeComponent();

            // Create sample data


        }

        public void fetchContract()
        {
            string query = $"SELECT * from  Contract WHERE id_worker = {this._worker._id_user}";
        }
    }
}
