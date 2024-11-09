using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels
{
    class ProposalPageViewModel
    {
        public ObservableCollection<Proposal> Proposals { get; set; }

        public ProposalPageViewModel()
        {
            // Initialize with dummy data for 5 proposals
            Proposals = new ObservableCollection<Proposal>
            {
                new Proposal("1"),
                new Proposal("2"),
                new Proposal("3"),
                new Proposal("4"),
                new Proposal("5"),
                new Proposal("6"),
                new Proposal("7"),
                new Proposal("8"),
                new Proposal("9")
            };
        }
    }
}
