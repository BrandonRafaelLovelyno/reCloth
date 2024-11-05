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

            Console.WriteLine(query);

            using (NpgsqlDataReader res = dbHelper.executeQuery(query))
            {
                if (res == null || !res.Read()) throw new Exception("Invalid credentials"); 
                
                string userId = res["id_user"].ToString(); 
                if (string.IsNullOrEmpty(userId)) throw new Exception("User Id not found"); 
                return userId;
            }
        }

        private void signIn(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = tbEmail.Text;
                string password = tbPassword.Password;

                string userId = fetchUserId(email, password);
                UserSession.Current.UserId = userId;

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
