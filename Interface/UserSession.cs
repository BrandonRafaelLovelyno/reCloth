using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    internal class UserSession
    {
        public static UserSession Current { get; private set; } = new UserSession();

        public string UserId { get; set; }

        public static void ClearSession()
        {
            Current = new UserSession();
        }
    }
}
