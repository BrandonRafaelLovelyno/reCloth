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
            switch (Id)
            {
                case "1":
                    Name = "Overfit dress into slimfit dress";
                    OrderInformation = "Design adjustment for slimming a dress";
                    Role = "Designer";
                    OrderBudget = "Rp. 300.000,00";
                    break;

                case "2":
                    Name = "Develop a mobile application";
                    OrderInformation = "Development of a mobile app for e-commerce";
                    Role = "Developer";
                    OrderBudget = "Rp. 1.500.000,00";
                    break;

                case "3":
                    Name = "Architectural design for office";
                    OrderInformation = "Complete office building design";
                    Role = "Architect";
                    OrderBudget = "Rp. 5.000.000,00";
                    break;

                case "4":
                    Name = "Consultation for marketing strategy";
                    OrderInformation = "Marketing strategy for online business";
                    Role = "Consultant";
                    OrderBudget = "Rp. 1.000.000,00";
                    break;

                case "5":
                    Name = "Photography session for product catalog";
                    OrderInformation = "Photography session for new clothing line";
                    Role = "Photographer";
                    OrderBudget = "Rp. 800.000,00";
                    break;

                default:
                    Name = "Generic Proposal";
                    OrderInformation = "General description of proposal";
                    Role = "Generalist";
                    OrderBudget = "Rp. 500.000,00";
                    break;
            }
        }
    }
}
