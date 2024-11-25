using Interface.Helpers;
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

namespace Interface
{
    /// <summary>
    /// Interaction logic for ProposalDetailed.xaml
    /// </summary>
    public partial class ProposalDetailed : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public string ContractId { get; set; }
        public ProposalDetailed(string proposalId)
        {
            InitializeComponent();
            ContractId = proposalId;
            this.DataContext = new ProposalDetailedViewModel(proposalId);
        }

        public void rejectProposal(object sender, RoutedEventArgs e)
        {
            string query = $"UPDATE contracts SET status = 'Rejected' WHERE id_contract = '{ContractId}';";
            dbHelper.executePostQuery(query);
            MessageBox.Show("Contract Rejected!", "Rejected", MessageBoxButton.OK, MessageBoxImage.Information);
            var appWindow = Application.Current.MainWindow as AppWindow;
            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new DashboardCustomer());
            }
        }

        public void acceptProposal(object sender, RoutedEventArgs e)
        {
            string query = $"UPDATE contracts SET status = 'Accepted' WHERE id_contract = '{ContractId}';";
            dbHelper.executePostQuery(query);
            MessageBox.Show("Contract Accepted!", "Accepted", MessageBoxButton.OK, MessageBoxImage.Information);
            var appWindow = Application.Current.MainWindow as AppWindow;
            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new DashboardCustomer());
            }
        }
    }
}
