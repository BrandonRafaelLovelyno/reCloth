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
        public DashboardWorker()
        {
            _worker = new Worker(UserSession.Current.UserId);
            InitializeComponent();
        }

        
    }
}
