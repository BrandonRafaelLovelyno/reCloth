using CloudinaryDotNet;
using Interface.Helpers;
using Interface.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Interface.Views
{
    /// <summary>
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private string selectedImagePath;
        private string imageUrl;
        private Cloudinary cloudinary;
        private OrderPageViewModel _viewModel;
        public string Id { get; private set; }
        public string _orderId { get; private set; }
        public UpdatePage(string orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            var config = GetConfiguration();
            Account account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
                );
            cloudinary = new Cloudinary(account);
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

                Upload_Border.Visibility = Visibility.Collapsed;
                Upload_Button.Visibility = Visibility.Collapsed;


                Change_Border.Visibility = Visibility.Visible;
                Change_Button.Visibility = Visibility.Visible;
            }
        }
        private void Change_Image(object sender, MouseButtonEventArgs e)
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

        private void cancelUpdate(object sender, RoutedEventArgs e)
        {
            var appWindow = Application.Current.MainWindow as AppWindow;
            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new OrderPage(_orderId));
            }
        }

        private async void submitUpdate(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedImagePath))
            {
                MessageBox.Show("Please upload an image before submitting.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Step 1: Upload the selected image to Cloudinary
                var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
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

                // Step 2: Update the order with the new image URL in the database
                string updateQuery = "UPDATE orders SET image = @image WHERE id_order = @orderId";

                Guid orderId = Guid.Parse(_orderId);

                var parameters = new Npgsql.NpgsqlParameter[]
                {
                new Npgsql.NpgsqlParameter("@image", imageUrl),
                new Npgsql.NpgsqlParameter("@orderId", orderId)
                };

                dbHelper.executePostQuery(updateQuery, parameters);

                // Step 3: Provide feedback to the user
                MessageBox.Show("Order successfully updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Step 4: Navigate back to the OrderPage
                var appWindow = Application.Current.MainWindow as AppWindow;
                if (appWindow != null)
                {
                    appWindow.MainFrame.NavigationService.Navigate(new OrderPage(_orderId));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
