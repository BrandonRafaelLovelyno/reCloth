using CloudinaryDotNet;
using Interface.Helpers;
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
        public UpdatePage()
        {
            InitializeComponent();
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
               
            }
        }

        private void submitUpdate(object sender, RoutedEventArgs e)
        {

        }
    }
}
