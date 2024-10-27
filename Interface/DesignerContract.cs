using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    class DesignerContract : Contract 
    {
        private Worker designer;
        private string result;

        public void postResult(string result)
        {
            this.result = result;
        }

    }
}