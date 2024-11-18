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

namespace Interface
{
    /// <summary>
    /// Interaction logic for FormWorker.xaml
    /// </summary>
    public partial class FormWorker : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public FormWorker()
        {
            InitializeComponent();
        }      
        private void Propose_Order(object sender, EventArgs e)
        {
            string specification = tbSpecificationWorker.Text;
            double budget;

            string budgetInput = tbBugdetWorker.Text.Replace(",", "").Replace(".", "");
            if (!double.TryParse(budgetInput,NumberStyles.Any, CultureInfo.InvariantCulture, out budget))
            {
                budgetInput = tbBugdetWorker.Text.Replace(",", "").Replace(".", "");
                double.TryParse(budgetInput, NumberStyles.Any, CultureInfo.InvariantCulture, out budget);
            }

            try
            {
                string insertQuery = "INSERT INTO proposals ( is_accepted. specification, budget) VALUES (@is_accepted,@specification,@budget)";

                var parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@is_accepted0", false),
                    new NpgsqlParameter("@specification", specification),
                    new NpgsqlParameter("@budget", budget )
                };

                dbHelper.executePostQuery(insertQuery, parameters);
            }
            catch (Exception ex) 
            { 
                MessageBox.Show($"An error occured: {ex.Message}", "Error", MessageBoxButton.OK,MessageBoxImage.Error);            
            }
        }
    }
}
