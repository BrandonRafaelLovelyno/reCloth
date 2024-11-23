using Interface.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Models
{
    internal class Proposal
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public string Id { get; private set; }
        public string? IdOrder { get; private set; }
        public string? Specification { get; set; }
        public string? Budget { get; set; }
        public Worker? Worker { get; set; }
        public string? Result{ get; set; }
        public bool? isAccepted{ get; set; }

        public Proposal(string id)
        {
            Id = id;
            fetchProposal();
        }

        private void fetchProposal()
        {
            string query = $"SELECT * FROM contracts  WHERE id_contract = '{Id}';";
            
            var rows = dbHelper.executeGetQuery(query,"id_order","specification","budget","id_worker","result","is_accepted");

            IdOrder = dbHelper.convertObject<Guid>(rows[0]["id_order"]).ToString();
            Specification = dbHelper.convertObject<string>(rows[0]["specification"]).ToString();
            Budget = dbHelper.convertObject<Double>(rows[0]["budget"]).ToString();
            Result = dbHelper.convertObject<string>(rows[0]["result"]).ToString();
            isAccepted = dbHelper.convertObject<bool>(rows[0]["is_accepted"]);

            string workerId = dbHelper.convertObject<Guid>(rows[0]["id_worker"]).ToString();
            query = $"SELECT * FROM workers where id_worker = '{workerId}';";

            rows = dbHelper.executeGetQuery(query, "id_user");

            string userId = dbHelper.convertObject<Guid>(rows[0]["id_user"]).ToString();
            Worker = new Worker(userId);
        }
    }
}
