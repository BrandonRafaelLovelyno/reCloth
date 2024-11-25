using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Helpers;
using Interface.Models;

namespace Interface.ViewModels
{
    class ProposalPageViewModel
    {
        public DatabaseHelper dbHelper = new DatabaseHelper();
        public string OrderId {  get; private set; }
        public ObservableCollection<Proposal> Proposals { get; set; }

        public ProposalPageViewModel(string orderId)
        {
            OrderId = orderId;
            Proposals = fetchContracts(orderId);

        }

        private ObservableCollection<Proposal> fetchContracts(string orderId)
        { 
            ObservableCollection<Proposal> retVal = new ObservableCollection<Proposal>();

            string query = $"SELECT * FROM contracts WHERE id_order = '{orderId}' AND status = 'Proposed';";

            var rows = dbHelper.executeGetQuery(query,"id_contract");

            foreach (var row in rows) {
                string idContract = dbHelper.convertObject<Guid>(row["id_contract"]).ToString();
                retVal.Add(new Proposal(idContract));
            }

            return retVal;
        }
    }
}
