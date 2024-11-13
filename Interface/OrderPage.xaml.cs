using Interface.ViewModels;
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
    public partial class OrderPage : Page
    {
        private OrderPageViewModel _viewModel;

        public OrderPage(string orderId)
        {
            InitializeComponent();
            _viewModel = new OrderPageViewModel(orderId);
            DataContext = _viewModel; // Set the DataContext to the ViewModel
        }

        private void Route_to_Form(object sender, MouseButtonEventArgs e)
        {
            var appWindow = Application.Current.MainWindow as AppWindow;

            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new ProposalPage());
            }
        }

    }
}
