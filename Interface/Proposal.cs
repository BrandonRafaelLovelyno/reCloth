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
        public string? Budget { get; set; }
        public string? Role { get; set; }
    
        public Proposal(string id)
        {
            Id = id;
            fetchProposal();
        }

        private void fetchProposal()
        {
            switch (Id)
            {
                case "1":
                    Name = "Overfit dress into slimfit dress";
                    Budget = "Rp. 300.000,00";
                    Role = "Designer";
                    break;

                case "2":
                    Name = "Develop a mobile application";
                    Budget = "Rp. 1.500.000,00";
                    Role = "Developer";
                    break;

                case "3":
                    Name = "Architectural design for office";
                    Budget = "Rp. 5.000.000,00";
                    Role = "Architect";
                    break;

                case "4":
                    Name = "Consultation for marketing strategy";
                    Budget = "Rp. 1.000.000,00";
                    Role = "Consultant";
                    break;

                case "5":
                    Name = "Photography session for product catalog";
                    Budget = "Rp. 800.000,00";
                    Role = "Photographer";
                    break;

                default:
                    Name = "Generic Proposal";
                    Budget = "Rp. 500.000,00";
                    Role = "Generalist";
                    break;
            }
        }
    }
}
