using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Helpers;

namespace Interface.Models
{
    class Contract
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private string Id { get; }
        public string? IdWorker { get; private set; }
        public string? IdOrder { get; private set; }
        public string? Status { get; private set; }
        public double? Budget { get; private set; }
        public string? Specification { get; private set; }
        public string? Result { get; private set; }

        public Contract(string id)
        {
            Id = id;
            fetchContract();
        }

        public string fetchUserId()
        {
            string query = $@"
                SELECT u.*
                FROM contracts c
                JOIN workers w ON c.id_worker = w.id_worker
                JOIN users u ON w.id_user = u.id_user
                WHERE c.id_contract = '{Id}';
            ";

            var rows = dbHelper.executeGetQuery(query, "id_user");

            string idUser = dbHelper.convertObject<Guid>(rows[0]["id_user"]).ToString();

            return idUser;
        }

        private void fetchContract()
        {
            string query = $"SELECT * from contracts WHERE id_contract = '{Id}';";

            var rows = dbHelper.executeGetQuery(query, "id_order", "id_worker", "result", "status", "budget", "specification");

            IdWorker = dbHelper.convertObject<Guid>(rows[0]["id_worker"]).ToString();
            IdOrder = dbHelper.convertObject<Guid>(rows[0]["id_order"]).ToString();
            Result = dbHelper.convertObject<string>(rows[0]["result"]);
            Status = dbHelper.convertObject<string>(rows[0]["status"]);
            Budget = dbHelper.convertObject<double>(rows[0]["budget"]);
            Specification = dbHelper.convertObject<string>(rows[0]["specification"]);
        }

    }
}