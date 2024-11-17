using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    internal class Proposal
    {
        public string Id { get; }
        public string? Name { get; set; }
        public string? OrderInformation { get; set; }
        public string? Budget { get; set; }
        public string? Role { get; set; }
        public string? OrderBudget { get; set; }

        public Proposal(string id)
        {
            Id = id;
            fetchProposal();
        }

        private void fetchProposal()
        {
            string query = $"SELECT * FROM proposals  WHERE id_proposal = {Id}";
        }
    }
}
