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

            dbHelper.executeQuery(query);

            string? idUser = dbHelper.extractValue<string>("id_user");

            if(idUser == null )
            {
                throw new Exception("Cannot find user by contract");
            }

            return idUser;
        }

        private void fetchContract()
        {
            string query = $"SELECT * from contracts WHERE id_contract = {Id}";

            dbHelper.executeQuery(query);

            IdProposal = dbHelper.extractValue<string>("id_proposal");
            IdWorker = dbHelper.extractValue<string>("id_worker");
            Result = dbHelper.extractValue<string>("result");
        }

    }
}