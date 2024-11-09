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
        public string? Spefication { get; private set; }
        public string? Image { get; private set; }
        public double? Budget { get; private set; }

        public Order(string id)
        {
            Id = id;
            fetchOrder();
        }

        public void fetchOrder()
        {
            string query = $"SELECT * from orders WHERE id_order = {Id}";

            dbHelper.executeQuery(query);

            Title = dbHelper.extractValue<string>("title");
            Spefication = dbHelper.extractValue<string>("spesification");
            Image = dbHelper.extractValue<string>("image");
            Budget = dbHelper.extractValue<double>("budget");
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

            dbHelper.executeQuery(query);

            return dbHelper.extractValue<string>("id_contract");
        }

    }
}
