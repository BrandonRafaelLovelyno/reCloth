using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

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
    }
}
