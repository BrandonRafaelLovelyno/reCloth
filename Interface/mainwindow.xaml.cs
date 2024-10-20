using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Set the window to fullscreen
            this.WindowState = WindowState.Maximized;

            MainFrame.Navigate(new SignIn());
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SignIn());
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SignUp());
        }

        private void App_Click(object sender, RoutedEventArgs e)
        {
            AppWindow appWindow = new AppWindow();

            appWindow.Show();

            this.Close();
        }
    }
}