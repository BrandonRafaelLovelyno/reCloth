using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reCloth
{
    class Contract
    {
        private Order order;
        private Proposal proposal;
        private Payment payment;

        public string Title { get; set; }
        public string Budget { get; set; }
        public string Status { get; set; }

        public void pay(int nominal)
        {
            
        }
    }
}