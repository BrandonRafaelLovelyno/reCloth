using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

namespace Interface
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public SignIn()
        {
            InitializeComponent();
        }

        private void Navigate_to_SignUp(object sender, MouseEventArgs e)
        {
            NavigationService.Navigate(new SignUp());
        }

        private string fetchUserId(string email, string password)
        {
            string hashedPassword = HashHelper.Hash(password);
            string query = $"SELECT * from users WHERE password = '{hashedPassword}' AND email = '{email}'";

            var rows = dbHelper.executeGetQuery(query, "id_user");

            if(rows.Count == 0)
            {
                throw new Exception("Invalid credentials");
            }

            string userId = dbHelper.convertObject<Guid>(rows[0]["id_user"]).ToString();

            return userId;
        }

        private bool checkUserCustomer(string userId)
        {
            bool isCustomer = false;

            string query = $"SELECT * from customers where id_user = '{userId}';";

            var rows = dbHelper.executeGetQuery(query);

            if (rows.Count > 0)
            {
                isCustomer = true;
            }

            return isCustomer;
        }

        private string getWorkersRole(string userId)
        {
            string role = "Tailor";

            string query = $"SELECT * from workers where id_user = '{userId}';";

            var rows = dbHelper.executeGetQuery(query, "role");

            if (dbHelper.convertObject<string>(rows[0]["role"]) == "Designer")
            {
                role = "Designer";
            }
            
            return role;
        }


        private void setUserSession(string email, string password)
        {
            string userId = fetchUserId(email, password);
            string role = "Worker";
            
            bool isCustomer = checkUserCustomer(userId);
            if(isCustomer)
            {
                role = "Customer";
            } else
            {
                role = getWorkersRole(userId);
            }

            UserSession.Current.UserId = userId;
            UserSession.Current.Role = role;
        }

        private void signIn(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = tbEmail.Text;
                string password = tbPassword.Password;

                setUserSession(email, password);

                MessageBox.Show("You are logged in!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                AppWindow appWindow = new AppWindow();
                appWindow.Show();

                Window.GetWindow(this)?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Occurred", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
