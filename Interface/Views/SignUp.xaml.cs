﻿using Interface.Helpers;
using Interface.Models;
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
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new Exception("Enter a valid email address.");

            string password = tbPassword.Password;
            string phoneNumber = tbPhoneNumber.Text;
            if (string.IsNullOrWhiteSpace(phoneNumber) || !phoneNumber.All(char.IsDigit))
                throw new Exception("Phone number must only contain numbers.");

            string address = tbAddress.Text;
           
            if (cbRole.SelectedItem == null ||  string.IsNullOrWhiteSpace(cbRole.SelectedItem.ToString())) 
            {
                throw new Exception("Roll is not selected!");
            }
            string? role = cbRole.SelectionBoxItem.ToString();

            if (password.Length < 8) throw new Exception("Password at least contain 8 characters");
                          
            return (name, email, password, phoneNumber, address, role);
        }

        private (string idUser, string name) createUser((string Name, string Email, string Password, string PhoneNumber, string Address, string Role) userInfo)
        {
            string hashedPassword = HashHelper.Hash(userInfo.Password);

            string query = $"INSERT INTO users (name, email, password, phone_number, address) VALUES ('{userInfo.Name}', '{userInfo.Email}', '{hashedPassword}', '{userInfo.PhoneNumber}', '{userInfo.Address}');";

            dbHelper.executePostQuery(query);

            query = $"SELECT * FROM users WHERE email = '{userInfo.Email}';";

            var rows = dbHelper.executeGetQuery(query, "id_user", "name");
            
            string idUser = dbHelper.convertObject<Guid>(rows[0]["id_user"]).ToString();
            string name = dbHelper.convertObject<string>(rows[0]["name"]).ToString();

            return (idUser, name);
        }

        private void createWorker(string idUser, string role)
        {
            string query = $"INSERT INTO workers (id_user, role) VALUES ('{idUser}', '{role}');";
            dbHelper.executePostQuery(query);
        }

        private void createCustomer(string idUser)
        {
            string query = $"INSERT INTO customers (id_user) VALUES ('{idUser}');";
            dbHelper.executePostQuery(query);

        }

        private void checkUserExist(string email, string name)
        {
            string query = $"SELECT * FROM users WHERE email = '{email}' OR name = '{name}';";

            var rows = dbHelper.executeGetQuery(query, "id_user");

            if(rows.Count == 1)
            {
                throw new Exception("Email or Name has been used");
            }
        }

        private void PhoneNumber_Preview(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Allow only numeric input for the phone number field
            e.Handled = !IsTextNumeric(e.Text);
        }
        private void Pasting_Number(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string pastedText = (string)e.DataObject.GetData(DataFormats.Text);
                if (!IsTextNumeric(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
        private static bool IsTextNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        private void setSession(string idUser, string name, string role)
        {
            UserSession.Current.UserId = idUser;
            UserSession.Current.Name = name;
            UserSession.Current.Role = role;
        }

        private void signUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var tbValues = extractTextBoxValues();

                checkUserExist(tbValues.Email, tbValues.Name);

                (string idUser, string name) = createUser(tbValues);

                string? role = cbRole.SelectionBoxItem.ToString();

                if(role == null)
                {
                    throw new Exception("Role is not selected");
                }

                if (tbValues.Role == "Customer")
                {
                    createCustomer(idUser);
                }
                else
                {
                    createWorker(idUser, tbValues.Role);
                }

                setSession(idUser, name, role);

                MessageBox.Show("User created!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

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
