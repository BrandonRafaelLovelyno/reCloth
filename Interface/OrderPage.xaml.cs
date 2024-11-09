using Npgsql;
using System;
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
    public partial class OrderPage : Page
    {
        public int OrderId { get; set; }   
        public string? OrderName { get; set; }
        public string? OrderSpecification { get; set; }
        public string? OrderImage { get; set; }
        public string? OrderBudget{ get; set; }

        
        public string? CustomerName {  get; set; }

        
        public string? DesignerName { get; set; }
        public string? DesignerOrderProcess { get; set; }
        public string? DesignerResult{ get; set; }
        public string? DesignerPayment { get; set; }


        public string? TaylorName { get; set; }
        public string? TaylorOrderProcess { get; set; }
        public string? TaylorResult { get; set; }
        public string? TaylorPayment { get; set; }



        public OrderPage(int orderId)
        {
            OrderId = orderId;
            InitializeComponent();
            fetchOrder();
            fetchDesigner();
            fetchCustomer();
            fetchTaylor();
            this.DataContext = this; 
        }

        private void fetchOrder() 
        { 
            Title = "Overfit dress into slimfit dress";
            OrderSpecification = "I need this dress, especially its arm, to be narrowed down. I need this dress, especially its arm, to be narrowed down.";
            OrderImage = "https://drive.google.com/abc";
            OrderBudget = "Rp. 300.000,00";
        }

        private void fetchCustomer()
        {
            CustomerName = "Harundoyo";
        }

        private void fetchDesigner()
        {
            DesignerName = "Brandon Rafael Lovelyno";
            DesignerOrderProcess = "Done";
            DesignerResult = "https://drive.google.com/abc";
            DesignerPayment = "https://drive.google.com/abc";
        }

        private void fetchTaylor()
        {
            TaylorName = "-";
            TaylorOrderProcess = "Not started";
            TaylorResult = "-";
            TaylorPayment = "-";
        }

        private void Route_to_Form(object sender, MouseButtonEventArgs e)
        {
            var appWindow = Application.Current.MainWindow as AppWindow;
            
            if (appWindow != null)
            {
                appWindow.MainFrame.NavigationService.Navigate(new FormWorker());
                
            }
        }
    }
}
