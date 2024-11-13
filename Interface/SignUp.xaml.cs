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

        private (string Name, string Email, string Password, string PhoneNumber, string Address, string Role) extractTextBoxValues()
        {
            string name = tbName.Text;
            string email = tbEmail.Text;
            string password = tbPassword.Text;
            string phoneNumber = tbPhoneNumber.Text;
            string address = tbAddress.Text;
            string role = "Tailor";

            return (name, email, password, phoneNumber, address, role);
        }

        //private (string Name, string userId) createUser((string Name, string Email, string Password, string PhoneNumber, string Address, string Role) userInfo)
        //{
        //    string hashedPassword = HashHelper.Hash(userInfo.Password);

        //    string query = $"INSERT INTO users (name, email, password, phone_number, address) VALUES ('{userInfo.Name}', '{userInfo.Email}', '{hashedPassword}', '{userInfo.PhoneNumber}', '{userInfo.Address}') RETURNING id_user, name;";

        //    using (NpgsqlDataReader res = dbHelper.executeQuery(query)) 
        //    { 
        //        if (res == null || !res.Read()) throw new Exception("Failed to insert user");

        //        string userId = res["id_user"].ToString();
        //        if (string.IsNullOrEmpty(userId)) throw new Exception("User Id not found"); 

        //        string name = res["name"].ToString();
        //        if (string.IsNullOrEmpty(name)) throw new Exception("Name not found");

        //        return (name, userId);
        //    }
        //}

        //private void createWorker(string idUser, string role)
        //{
        //    string query = $"INSERT INTO workers (id_user, role) VALUES ('{idUser}', '{role}')";
        //    dbHelper.executeQuery(query);
        //}

        //private void createCustomer(string idUser)
        //{
        //    string query = $"INSERT INTO customers (id_user) VALUES ('{idUser}')";
        //    dbHelper.executeQuery(query);

        //}

        private void signUp(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    var tbValues = extractTextBoxValues();

            //    var user = createUser(tbValues);
            //    UserSession.Current.UserId = user.userId;

            //    if(tbValues.Role == "Customer")
            //    {
            //        createCustomer(user.userId);
            //    }
            //    else
            //    {
            //         createWorker(user.userId, tbValues.Role);
            //    }

            //    MessageBox.Show("User created!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error Occurred", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }
    }
}
