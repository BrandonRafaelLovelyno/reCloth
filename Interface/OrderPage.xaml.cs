using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Interface
{
    public partial class OrderPage : Page
    {
        private Order _order;

        private Contract? _tailorContract;
        private Contract? _designerContract;

        private User? _tailorUser;
        private User? _designerUser;

        public OrderPage(string orderId)
        {
            _order = new Order(orderId);
            fetchWorkerUser("tailor");
            fetchWorkerUser("designer");

            InitializeComponent();
        }

        private void fetchWorkerUser(string role)
        {
            fetchAcceptedContract(role);
            
            if(role == "tailor" && _tailorContract != null)
            {
                string userId = _tailorContract.fetchUserId();
                _tailorUser = new User(userId);
            }
            else if(role == "designer" && _designerContract != null)
            {
                string userId = _designerContract.fetchUserId();
                _designerUser = new User(userId);
            }
        }

        private void fetchAcceptedContract(string role)
        {
            string? contractId = _order.findAcceptedContract(role);

            if (contractId != null)
            {
                if (role == "tailor")
                {
                    _tailorContract = new Contract(contractId);
                }

                if (role == "designer")
                {
                    _designerContract = new Contract(contractId);
                }
            }
        }

    }
}
