﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    internal class Worker : User
    {
        public string _id_user;
        public string _role;

        public Worker(string id_user, string role)
        {
            _id_user = id_user;
            _role = role;
        }
    }
}