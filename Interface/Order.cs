using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public class Order
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public string _title;
        public double _budget;
        public bool _is_done;

        public Order(string title, double budget, bool is_done)
        {
            _title = title;
            _budget = budget;
            _is_done = is_done;
        }

        public void postImage(string image)
        {
            string query = $"UPDATE Order SET image = '{image}'";
            dbHelper.executeQuery(query);
        }
       
    }
}
