using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    class User
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        private string Id { get; }
        public string? Name { get; private set; }
        public string? Address { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }

        public User(string id)
        {
            Id = id;
            fetchUser();
        }

        private void fetchUser()
        {
            string query = $"SELECT * from orders WHERE id_order = {Id}";
            dbHelper.executeQuery(query);

            Name = dbHelper.extractValue<string>("name");
            Address = dbHelper.extractValue<string>("address");
            PhoneNumber = dbHelper.extractValue<string>("phone_number");
            Email = dbHelper.extractValue<string>("email");
            Password = dbHelper.extractValue<string>("password");
        }

    }
}