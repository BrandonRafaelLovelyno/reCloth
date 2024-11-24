using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Interface.Helpers;
using Interface.Models;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace Interface.ViewModels
{
    internal class OrderPageViewModel : INotifyPropertyChanged
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        private Order _order;
        public Order Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        private Contract _designerContract;
        public Contract DesignerContract
        {
            get { return _designerContract; }
            set
            {
                _designerContract = value;
                OnPropertyChanged(nameof(DesignerContract));
            }
        }

        private Contract _tailorContract;
        public Contract TailorContract
        {
            get { return _tailorContract; }
            set
            {
                _tailorContract = value;
                OnPropertyChanged(nameof(TailorContract));
            }
        }

        private Customer _customerUser;
        public Customer Customer
        {
            get { return _customerUser; }
            set
            {
                _customerUser = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        private Worker _designer;
        public Worker Designer
        {
            get { return _designer; }
            set
            {
                _designer = value;
                OnPropertyChanged(nameof(Designer));
            }
        }

        private Worker _tailor;
        public Worker Tailor
        {
            get { return _tailor; }
            set
            {
                _tailor = value;
                OnPropertyChanged(nameof(Tailor));
            }
        }

        private bool isOrderOwner;
        public bool IsOrderOwner
        {
            get => isOrderOwner;
            set
            {
                isOrderOwner = value;
                OnPropertyChanged(nameof(IsOrderOwner));
            }
        }

        private bool isContractAccepted;
        public bool IsContractAccepted
        {
            get => isContractAccepted;
            set
            {
                isContractAccepted = value;
                OnPropertyChanged(nameof(IsContractAccepted));
            }
        }

        private bool isCanApplyWork;
        public bool IsCanApplyWork
        {
            get => isCanApplyWork;
            set
            {
                isCanApplyWork = value;
                OnPropertyChanged(nameof(isCanApplyWork));
            }
        }
        public ICommand DeleteOrderCommand { get; }

        public OrderPageViewModel(string orderId)
        {
            Order = new Order(orderId);
            FetchWorkerUser("Designer");
            FetchWorkerUser("Tailor");
            FetchCustomerUser();

            IsOrderOwner = checkIsOrderOwner();
            isCanApplyWork = checkIsCanApplyWork(orderId);
            IsContractAccepted = checkIsContractAccepted();

            DeleteOrderCommand = new RelayCommand<object>(_ => Delete_Order(orderId));
        }

        public bool checkIsCanApplyWork(string orderId)
        {
            if(UserSession.Current.Role == "Customer")
            {
                return false;
            }

            string query = $"SELECT * FROM contracts" +
                $" JOIN orders ON orders.id_order = contracts.id_order" +
                $" JOIN workers ON workers.id_worker = contracts.id_worker" +
                $" JOIN users ON users.id_user = workers.id_user" +
                $" WHERE orders.id_order = '{orderId}' AND users.id_user = '{UserSession.Current.UserId}'" +
                $";";

            var rows = dbHelper.executeGetQuery(query, "status");

            foreach (var row in rows)
            {
                string contractStatus = dbHelper.convertObject<string>(row["status"]);
                if(contractStatus != "Rejected")
                {
                    return false ;
                }
            }

            return true;

        }

        private bool checkIsContractAccepted()
        {
           if (UserSession.Current.Role == "Customer" || UserSession.Current.UserId == null) return false;

            Worker currentWorker = new Worker(UserSession.Current.UserId);

            if(UserSession.Current.Role == "Tailor" && TailorContract != null)
            {
                if(currentWorker.Id == TailorContract.IdWorker && TailorContract.Status == "Accepted")
                {
                    return true;

                }
            }
            else if(UserSession.Current.Role == "Designer" && DesignerContract != null)
            {
                if (currentWorker.Id == DesignerContract.IdWorker && DesignerContract.Status == "Accepted")
                {
                    return true;
                }
            }
            return false;
        }
        private bool checkIsOrderOwner()
        {
            if (UserSession.Current.Role != "Customer" || UserSession.Current.UserId == null) return false;

            Customer currentCustomer = new Customer(UserSession.Current.UserId);

            return currentCustomer.Id == _order.IdCustomer;
        }

        private void FetchCustomerUser()
        {
            string query = $"SELECT * FROM orders " +
                $"JOIN customers on customers.id_customer = orders.id_customer " +
                $"JOIN users on users.id_user = customers.id_user WHERE id_order = '{_order.Id}';";

            var rows = dbHelper.executeGetQuery(query, "id_user");    

            string userId = dbHelper.convertObject<Guid>(rows[0]["id_user"]).ToString();

            Customer = new Customer(userId);
        }

        private void FetchWorkerUser(string role)
        {
            findAcceptedOrDoneContract(role);

            if (role == "Designer" && DesignerContract != null)
            {
                string userId = DesignerContract.fetchUserId();
                Designer = new Worker(userId);
            }
            else if (role == "Tailor" && TailorContract != null)
            {
                string userId = TailorContract.fetchUserId();
                Tailor = new Worker(userId);
            }
        }

        private void findAcceptedOrDoneContract(string role)
        {
            string? contractId = Order.findAcceptedOrDoneContract(role);

            if (contractId != null)
            {
                if (role == "Designer")
                {
                    DesignerContract = new Contract(contractId);
                }
                else if (role == "Tailor")
                {
                    TailorContract = new Contract(contractId);
                }
            }
        }
        private void Delete_Order(string orderId)
        {
            Console.WriteLine("Deleting order");
            string query = $"DELETE from orders WHERE id_order = '{orderId}'; ";

            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this order?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Proceed with deletion if the user confirms
                    dbHelper.executePostQuery(query);
                    MessageBox.Show($"Order successfully deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Navigate back to DashboardCustomer
                    var appWindow = Application.Current.MainWindow as AppWindow;
                    if (appWindow != null)
                    {
                        appWindow.MainFrame.NavigationService.Navigate(new DashboardCustomer());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
