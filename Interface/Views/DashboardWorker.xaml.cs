using Interface.Models;
using Interface.ViewModels;
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
        private DashboardWorkerViewModel _viewModel;

        public DashboardWorker()
        {
            InitializeComponent();
            _viewModel = new DashboardWorkerViewModel(UserSession.Current.UserId);
            DataContext = _viewModel;
        }
    }
}
