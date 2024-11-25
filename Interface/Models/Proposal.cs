using Interface.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Models
{
    internal class Proposal : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public string Id { get; private set; }
        public string? IdOrder { get; private set; }
        public string? Specification { get; set; }
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
        public Worker? Worker { get; set; }
        public string? Result{ get; set; }
        public string? Status{ get; set; }

        public Proposal(string id)
        {
            Id = id;
            fetchProposal();
        }

        private void fetchProposal()
        {
            string query = $"SELECT * FROM contracts  WHERE id_contract = '{Id}';";
            
            var rows = dbHelper.executeGetQuery(query,"id_order","specification","budget","id_worker","result","status");

            IdOrder = dbHelper.convertObject<Guid>(rows[0]["id_order"]).ToString();
            Specification = dbHelper.convertObject<string>(rows[0]["specification"]).ToString();
            Budget = dbHelper.convertObject<Double>(rows[0]["budget"]);
            Result = dbHelper.convertObject<string>(rows[0]["result"]).ToString();
            Status = dbHelper.convertObject<string>(rows[0]["status"]);

            string workerId = dbHelper.convertObject<Guid>(rows[0]["id_worker"]).ToString();
            query = $"SELECT * FROM workers where id_worker = '{workerId}';";

            rows = dbHelper.executeGetQuery(query, "id_user");

            string userId = dbHelper.convertObject<Guid>(rows[0]["id_user"]).ToString();
            Worker = new Worker(userId);
        }

        public string FormattedBudget => Budget.HasValue ? $"Rp {Budget.Value:N0}" : "Rp 0";
    }
}
