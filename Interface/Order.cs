using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Interface
{
    public class Order
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        private string Id { get; }
        public string? Title { get; private set; }
        public string? Specification { get; private set; }
        public string? Image { get; private set; }
        public double? Budget { get; private set; }

        public Order(string id)
        {
            Id = id;
            fetchOrder();
        }

        public void fetchOrder()
        {
            string query = $"SELECT * from orders WHERE id_order = '{Id}' LIMIT 1;";

            var rows = dbHelper.executeGetQuery(query, "title", "specification", "image", "budget");

            Title = dbHelper.convertObject<string>(rows[0]["title"]);
            Specification = dbHelper.convertObject<string>(rows[0]["specification"]);
            Image = dbHelper.convertObject<string>(rows[0]["image"]);
            Budget = dbHelper.convertObject<double>(rows[0]["budget"]);
        }

        public string? findAcceptedContract(string role)
        {
            string query = $@"
            SELECT * 
            FROM contracts c
            JOIN proposals p ON c.id_contract = p.id_contract
            WHERE c.id_order = {Id} AND p.is_accepted = true
            LIMIT 1;
             ";

            var rows = dbHelper.executeGetQuery(query, "id_contract");

            return dbHelper.convertObject<Guid>(rows[0]["id_contract"]).ToString();
        }

    }
}
