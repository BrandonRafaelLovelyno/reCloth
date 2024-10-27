using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Interface
{
    class Portfolio
    {
        private string title;
        private string image;
        private string information;

        private DatabaseHelper dbHelper = new DatabaseHelper();

        public void postImage(string image)
        {
            this.image = image;
            string query = $"UPDATE Portfolio SET image = '{image}'";
            dbHelper.executeQuery(query);
        }
        public void changeTitle(string title)
        {
            this.title = title;
        }
        public void changeInformation(string information)
        {
            this.information = information;
        }
    }
}