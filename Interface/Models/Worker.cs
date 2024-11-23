using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Helpers;

namespace Interface.Models
{
    internal class Worker : User
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public string? Id { get; private set; }
        public string? Role { get; private set; }

        public Worker(string userId) : base(userId)
        {
            fetchWorker();
        }

        public void fetchWorker()
        {
            string query = $"SELECT * from workers WHERE id_user = '{IdUser}';";

            var rows = dbHelper.executeGetQuery(query, "id_worker", "role");

            Id = dbHelper.convertObject<Guid>(rows[0]["id_worker"]).ToString();
            Role = dbHelper.convertObject<string>(rows[0]["role"]);
        }

        public void fetchOrder()
        {
            string query = $"SELECT * from orders WHERE id_user = '{IdUser}'";

        }
    }
}
