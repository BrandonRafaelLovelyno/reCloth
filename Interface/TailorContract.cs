using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    internal class TailorContract : Contract
    {
        private Worker tailor;
        private string result;
        public void postResult(string result)
        {
            this.result = result;
        }
    }
}
