using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    class Contract
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private string Id { get; }
        public string? IdProposal { get; private set; }
        public string? IdWorker { get; private set; }
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
                WHERE c.id_worker = {Id};
            ";

            var rows = dbHelper.executeGetQuery(query, "id_user");

            string idUser = dbHelper.convertObject<Guid>(rows[0]["id_user"]).ToString();

            return idUser;
        }

        private void fetchContract()
        {
            string query = $"SELECT * from contracts WHERE id_contract = '{Id}'";

            var rows = dbHelper.executeGetQuery(query,"id_proposal","id_worker","result");

            IdProposal = dbHelper.convertObject<Guid>("id_proposal").ToString();
            IdWorker = dbHelper.convertObject<Guid>(rows[0]["id_worker"]).ToString();
            Result = dbHelper.convertObject<string>(rows[0]["result"]);
        }

    }
}