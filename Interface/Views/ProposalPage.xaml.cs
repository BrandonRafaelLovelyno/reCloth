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

namespace Interface
{
    /// <summary>
    /// Interaction logic for ProposalPage.xaml
    /// </summary>
    public partial class ProposalPage : Page
    {
        public string Id { get; private set; }
        public ProposalPage(string id)
        {
            Id = id;
            InitializeComponent();
            this.DataContext = new ProposalPageViewModel();
        }

        private void ShowProposalDetail(object sender, RoutedEventArgs e)
        {
            var border = sender as Border;
            if (border?.DataContext is Proposal selectedProposal)
            {
                string proposalId = selectedProposal.Id;
                Console.WriteLine($"Proposal Id: {proposalId}");

                // Navigate to ProposalDetailed and pass the proposalId
                ProposalDetailed proposalDetailedPage = new ProposalDetailed(proposalId);
                var appWindow = Application.Current.MainWindow as AppWindow;

                if (appWindow != null)
                {
                    appWindow.MainFrame.NavigationService.Navigate(proposalDetailedPage);

                }
            }
        }
    }
}
