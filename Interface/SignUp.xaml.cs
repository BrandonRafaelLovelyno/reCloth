using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Interface
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public SignUp()
        {
            InitializeComponent();
        }

        private void Navigate_to_SignIn(object sender, MouseEventArgs e)
        {
            NavigationService.Navigate(new SignIn());
        }
       
    }
}
