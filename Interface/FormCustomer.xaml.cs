using System;
using System.Collections.Generic;
using System.Globalization;
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
using Microsoft.Win32;
using Npgsql;
using System.IO;

namespace Interface
{
    /// <summary>
    /// Interaction logic for FormCustomer.xaml
    /// </summary>
    public partial class FormCustomer : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private string selectedImagePath;
        private byte[] selectedImageBytes;
        private string customerId;
        public FormCustomer()
        {
            InitializeComponent();
            Customer customer = new Customer(UserSession.Current.UserId);
            customerId = customer.Id;
        }

        private void SaveImageToDirectory(string sourcePath, string targetDirectory)
        {
            string fileName = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(targetDirectory, fileName);

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            File.Copy(sourcePath, targetPath, true);
            MessageBox.Show($"Image saved to: {targetPath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Upload_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                selectedImageBytes = File.ReadAllBytes(selectedImagePath);
                ImagePreview.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }

        private void Post_Order(object sender, RoutedEventArgs e)
        {
            string title = tbTitleCustomer.Text;
            string specification = tbSpecificationCustomer.Text;
            double budget;

            string budgetInput = tbBudgetCustomer.Text.Replace(",", "").Replace(".", "");
            if (!double.TryParse(budgetInput, NumberStyles.Any, CultureInfo.InvariantCulture, out budget))
            {
                MessageBox.Show("Please enter a valid budget value.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");
                SaveImageToDirectory(selectedImagePath, targetDirectory);

                string insertQuery = "INSERT INTO orders (id_customer, budget, image, specification, is_done, title) VALUES (@id_customer, @budget, @image, @specification, @is_done, @title)";

                Guid userId= Guid.Parse(customerId);

                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@id_customer", userId), // Corrected to use customer ID
                    new NpgsqlParameter("@budget", budget),
                    new NpgsqlParameter("@image", selectedImageBytes), // Image byte array
                    new NpgsqlParameter("@specification", specification),
                    new NpgsqlParameter("@is_done", false),
                    new NpgsqlParameter("@title", title)
                };

                dbHelper.executePostQuery(insertQuery, parameters); // Added to execute the query
                                                                    // 
                MessageBox.Show("Order successfully submitted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
