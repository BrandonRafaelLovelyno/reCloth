using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    internal class Worker : User
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        private string Id { get; }
        public string? UserId { get; private set; }

        public Worker(string id)
        {
            Id = id;
            fetchWorker();
        }

        public void fetchWorker()
        {
            string query = $"SELECT * from workers WHERE id_worker = {Id}";

            dbHelper.executeQuery(query);

            UserId = dbHelper.extractValue<string>("id_user");
        }
    }
}
