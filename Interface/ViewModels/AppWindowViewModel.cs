using Interface.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels
{
    class AppWindowViewModel : INotifyPropertyChanged
    {
        private bool isCustomer;
        public bool IsCustomer
        {
            get => isCustomer;
            set
            {
                isCustomer = value;
                
            }
        }

        public AppWindowViewModel()
        {
            IsCustomer = UserSession.Current.Role == "Customer";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
