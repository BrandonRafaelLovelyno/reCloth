using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Order> fetchWorkerOrders()
        {
            var orders = new ObservableCollection<Order>();

            string query = $"SELECT * from orders JOIN contracts ON contracts.id_order = orders.id_order WHERE contracts.id_worker = '{Id}' AND (contracts.status = 'Accepted' OR contracts.status = 'Done')";

            var rows = dbHelper.executeGetQuery(query,"id_order");

            foreach (var row in rows)
            {
                string orderId = dbHelper.convertObject<Guid>(row["id_order"]).ToString();
                orders.Add(new Order(orderId));
            }

            return orders;
        }

        public ObservableCollection<Contract> fetchWorkerContracts()
        {
            var contracts = new ObservableCollection<Contract>();

            string query = $"SELECT * from contracts where id_worker = '{Id}';";

            var rows = dbHelper.executeGetQuery(query, "id_contract");

            foreach (var row in rows)
            {
                string contractId = dbHelper.convertObject<Guid>("id_contract").ToString();
                contracts.Add(new Contract(contractId));
            }

            return contracts;
        }

        public List<Contract> fetchContracts()
        {
            List<Contract> contracts = new List<Contract>();
            string query = $"SELECT * from contracts WHERE id_worker = '{Id}';";

            var rows = dbHelper.executeGetQuery(query, "id_contract", "id_order");

            foreach (var row in rows)
            {
                contracts.Add(new Contract(dbHelper.convertObject<Guid>(row["id_contract"]).ToString()));
            }

            return contracts;
        }
    }
}
