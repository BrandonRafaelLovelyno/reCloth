using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Helpers;

namespace Interface.Models
{
    class User
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        public string IdUser { get; }
        public string? Name { get; private set; }
        public string? Address { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }

        public User(string id)
        {
            IdUser = id;
            fetchUser();
        }

        private void fetchUser()
        {
            string query = $"SELECT * from users WHERE id_user = '{IdUser}';";
            var rows = dbHelper.executeGetQuery(query, "name", "address", "phone_number", "email", "password");

            Name = dbHelper.convertObject<string>(rows[0]["name"]);
            Address = dbHelper.convertObject<string>(rows[0]["address"]);
            PhoneNumber = dbHelper.convertObject<string>(rows[0]["phone_number"]);
            Email = dbHelper.convertObject<string>(rows[0]["email"]);
            Password = dbHelper.convertObject<string>(rows[0]["password"]);
        }

    }
}