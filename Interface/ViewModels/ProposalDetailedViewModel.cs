using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Helpers;
using Interface.Models;

namespace Interface.ViewModels
{
    class ProposalDetailedViewModel
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public Proposal ProposalDetail { get; set; }
        public Worker ProposalWorker { get; set; }

        public ProposalDetailedViewModel(string proposalId)
        {
            ProposalDetail = new Proposal(proposalId);
        }
    }
}
