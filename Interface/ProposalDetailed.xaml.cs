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
        public ProposalDetailed(string proposalId)
        {
            InitializeComponent();
            this.DataContext = new ProposalDetailedViewModel(proposalId);
        }
    }
}
