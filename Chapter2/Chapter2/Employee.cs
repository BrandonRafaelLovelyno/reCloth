using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2
{
    class Employee
    {
        private int _empId;
        private string _loginName;
        private string _password;
        private int _securityLevel;

        public int EmployeeId
        {
            get { return _empId; }
        }

        public string loginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        public string Password
        { get { return _password; }
            set { _password = value; }
        }

        public int SecurityLevel
        { get { return _securityLevel; } }

        public Boolean Login(string loginName, string password)
        {
            if(loginName == "Brandon" & password == "Rafael")
            {
                _empId = 1;
                _securityLevel = 2;
                return true;
            }else if(loginName == "Deren" & password == "Tanaphan")
            {
                _empId = 2;
                _securityLevel = 4;
                return true;
            }
            else
            {
                return false;
            }
        }

    }


}
