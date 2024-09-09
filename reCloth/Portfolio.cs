using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reCloth
{
    class Portfolio
    {
        private string title;
        private string image;
        private string information;

        public void changeTitle(string title)
        {
            this.title = title;
        }

        public void changeImage(string image)
        {
            this.image = image;
        }

        public void changeInformation(string information)
        {
            this.information = information;
        }
    }
}