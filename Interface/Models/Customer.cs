using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Helpers;

namespace Interface.Models
{
    internal class Customer : User
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        public string IdUser { get; private set; }
        public string? Id { get; private set; }

        public Customer(string userId) : base(userId)
        {
            IdUser = userId;
            fetchCustomer();
        }

        private void fetchCustomer()
        {
            string query = $"SELECT * FROM customers WHERE id_user = '{IdUser}'";

            var rows = dbHelper.executeGetQuery(query, "id_customer");

            string customerId = dbHelper.convertObject<Guid>(rows[0]["id_customer"]).ToString();

            if (customerId == null) throw new Exception($"Cannot find customer with user id of {IdUser}");

            Id = customerId;
        }
    }
}
