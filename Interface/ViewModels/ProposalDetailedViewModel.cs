using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels
{
    class ProposalDetailedViewModel
    {
        public Proposal ProposalDetail { get; set; }

        public ProposalDetailedViewModel(string proposalId)
        {
            ProposalDetail = new Proposal(proposalId);
        }
    }
}
