using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    class User
    {
        private string name;
        private string phoneNumber;
        private string email;

        public void changePhone(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public void changeEmail(string email)
        {
            this.email = email;
        }

        public void login(string username, string password)
        {
            
        }

        public void signOut()
        {

        }
    }
}