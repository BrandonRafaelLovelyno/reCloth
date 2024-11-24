using Interface.Helpers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Interface.Models
{
    public class Order : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DatabaseHelper dbHelper = new DatabaseHelper();

        public string Id { get; private set; }
        public string? Title { get; private set; }
        public string? Specification { get; private set; }
        public string? Image { get; private set; }
        public string? DesignerStatus { get; set; }
        public string? TailorStatus { get; set; }
        private double? budget;
        public double? Budget
        {
            get => budget;
            set
            {
                budget = value;
                OnPropertyChanged(nameof(Budget));
                OnPropertyChanged(nameof(FormattedBudget)); 
            }
        }
        public string? IdCustomer {  get; private set; }

        public Order(string id)
        {
            Id = id;
            fetchOrder();
        }
        public void fetchOrder()
        {
            string query = $"SELECT * from orders WHERE id_order = '{Id}' LIMIT 1;";

            var rows = dbHelper.executeGetQuery(query, "title", "specification", "image", "budget","id_customer");

            Title = dbHelper.convertObject<string>(rows[0]["title"]);
            Specification = dbHelper.convertObject<string>(rows[0]["specification"]);
            Image = dbHelper.convertObject<string>(rows[0]["image"]);
            Budget = dbHelper.convertObject<double>(rows[0]["budget"]);
            IdCustomer = dbHelper.convertObject<Guid>(rows[0]["id_customer"]).ToString();
        }

        public string? findProposedContract(string role)
        {
            string query = $@"
            SELECT * 
            FROM contracts
            JOIN orders ON orders.id_order = '{Id}'
            JOIN workers ON workers.id_worker = contracts.id_worker
            WHERE contracts.status = 'Proposed' AND workers.role = '{role}';
            ";

            var rows = dbHelper.executeGetQuery(query, "id_contract");

            if (rows.Count() == 0) return null;

            return dbHelper.convertObject<Guid>(rows[0]["id_contract"]).ToString();
        }

        public string? findAcceptedContract(string role)
        {
            string query = $@"
            SELECT * 
            FROM contracts
            JOIN orders ON orders.id_order = contracts.id_order
            JOIN workers ON workers.id_worker = contracts.id_worker
            WHERE contracts.status = 'Accepted' AND workers.role = '{role}' AND orders.id_order = '{Id}';
            ";

            var rows = dbHelper.executeGetQuery(query, "id_contract");

            if (rows.Count() == 0) return null;

            return dbHelper.convertObject<Guid>(rows[0]["id_contract"]).ToString();
        }

        public string? findFinishedContract(string role)
        {
            string query = $@"
            SELECT * 
            FROM contracts
            JOIN orders ON orders.id_order = '{Id}'
            JOIN workers ON workers.id_worker = contracts.id_worker
            WHERE contracts.status = 'Finished'
            AND workers.role = '{role}'
            AND contracts.result IS NOT NULL;
            ";

            var rows = dbHelper.executeGetQuery(query, "id_contract");

            if (rows.Count() == 0) return null;

            return dbHelper.convertObject<Guid>(rows[0]["id_contract"]).ToString();
        }

        public string FormattedBudget => Budget.HasValue ? $"Rp {Budget.Value:N0}" : "Rp 0";
    }
}
