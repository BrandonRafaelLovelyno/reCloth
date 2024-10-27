using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    internal class TaylorContract : Contract
    {
        private Taylor taylor;
        private string result;
        public void postResult(string result)
        {
            this.result = result;
        }
    }
}
