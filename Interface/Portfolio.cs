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
        private string _id;

        private DatabaseHelper dbHelper = new DatabaseHelper();

        public Portfolio(string title, string image, string information,string id)
        {
            this.title = title;
            this.image = image;
            this.information = information;
            this._id = id;
        }

        public void postImage(string image) 
        {
            this.image = image;
            string query = $"UPDATE Portfolio SET image = '{image}' WHERE id = {_id}";

            //dbHelper.executeQuery(query); 
        }
    }
}