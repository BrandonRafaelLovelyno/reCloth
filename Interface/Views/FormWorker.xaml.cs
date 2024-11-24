using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Interface.Helpers;
using Interface.Models;
using Interface.Views;
using System.ComponentModel;

namespace Interface
{
    /// <summary>
    /// Interaction logic for FormWorker.xaml
    /// </summary>
    public partial class FormWorker : Page, INotifyPropertyChanged
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private string workerId;
        private string _orderId;
        private string workerName;
        public string WorkerName
        {
            get => workerName;
            set
            {
                workerName = value;
                OnPropertyChanged(nameof(workerName));
            }
        }

        public FormWorker(string orderId)
        {
            InitializeComponent();
            DataContext = this;
            WorkerName = $"Hello, {UserSession.Current.Name}!";
            Worker worker = new Worker(UserSession.Current.UserId);
            workerId = worker.Id;
            _orderId = orderId;
            Console.WriteLine(WorkerName);
        }      
        private void Propose_Order(object sender, EventArgs e)
        {
            string specification = tbSpecificationWorker.Text;
            double budget;
           

            string budgetInput = tbBugdetWorker.Text.Replace(",", "").Replace(".", "");
            if (string.IsNullOrWhiteSpace(budgetInput) || !budgetInput.All(char.IsDigit))
                throw new Exception("Phone number must only contain numbers.");
            if (!double.TryParse(budgetInput,NumberStyles.Any, CultureInfo.InvariantCulture, out budget))
            {
                budgetInput = tbBugdetWorker.Text.Replace(",", "").Replace(".", "");
                double.TryParse(budgetInput, NumberStyles.Any, CultureInfo.InvariantCulture, out budget);
            }

            try
            {
                string insertQuery = "INSERT INTO contracts ( id_order, id_worker, result, status, specification, budget) VALUES ( @id_order, @id_worker, @result ,@status,@specification,@budget)";

                Guid orderId = Guid.Parse(_orderId);
                Guid userId = Guid.Parse(workerId);
                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@id_order", orderId),
                    new NpgsqlParameter("@id_worker", userId),
                    new NpgsqlParameter("@result", "On going"),
                    new NpgsqlParameter("@status", "Proposed"),
                    new NpgsqlParameter("@specification", specification),
                    new NpgsqlParameter("@budget", budget )
                };

                dbHelper.executePostQuery(insertQuery, parameters);
                MessageBox.Show("Proposed success!","Success", MessageBoxButton.OK, MessageBoxImage.Information);

                var appWindow = Application.Current.MainWindow as AppWindow;
                if (appWindow != null)
                {
                    appWindow.MainFrame.NavigationService.Navigate(new DashboardWorker());
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show($"An error occured: {ex.Message}", "Error", MessageBoxButton.OK,MessageBoxImage.Error);            
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
