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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Interface.Models;
using Interface.Helpers;

namespace Interface
{
    /// <summary>
    /// Interaction logic for FormCustomer.xaml
    /// </summary>
    public partial class FormCustomer : System.Windows.Controls.Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private string selectedImagePath;
        private string imageUrl;
        private Cloudinary cloudinary;
        private string customerId;
        public FormCustomer()
        {
            InitializeComponent();
            var config = GetConfiguration();
            Account account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
                );
            cloudinary = new Cloudinary(account);
            Customer customer = new Customer(UserSession.Current.UserId);
            customerId = customer.Id;
        }

        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
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
                ImagePreview.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }

        private async void Post_Order(object sender, RoutedEventArgs e)
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
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(selectedImagePath)
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    imageUrl = uploadResult.SecureUrl.ToString();
                }
                else
                {
                    MessageBox.Show("Image upload failed. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string insertQuery = "INSERT INTO orders (id_customer, budget, image, specification, is_done, title) VALUES (@id_customer, @budget, @image, @specification, @is_done, @title)";

                Guid userId= Guid.Parse(customerId);

                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@id_customer", userId), // Corrected to use customer ID
                    new NpgsqlParameter("@budget", budget),
                    new NpgsqlParameter("@image", imageUrl), // Image byte array
                    new NpgsqlParameter("@specification", specification),
                    new NpgsqlParameter("@is_done", false),
                    new NpgsqlParameter("@title", title)
                };

                dbHelper.executePostQuery(insertQuery, parameters); // Added to execute the query
                                                                    // 
                MessageBox.Show("Order successfully submitted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                var appWindow = Application.Current.MainWindow as AppWindow;
                if (appWindow != null)
                {
                    appWindow.MainFrame.NavigationService.Navigate(new DashboardCustomer());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
