using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    internal class Customer : User
    {
        public string _id;
        public Customer(string id) 
        {
            _id = id;
        }
    }
}
