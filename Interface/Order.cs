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
        public string Title { get; set; }
        public string Budget { get; set; }
        public String Status { get; set; }

        public void postImage(string image)
        {
            string query = $"UPDATE Order SET image = '{image}'";
            dbHelper.executeQuery(query);
        }
       
    }
}
