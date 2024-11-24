using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CloudinaryDotNet.Actions;
using Interface.Helpers;
using Interface.Models;

namespace Interface.ViewModels
{
    internal class UpdateContractViewModel : INotifyPropertyChanged
    {
        private string _role = UserSession.Current.Role;
        private Worker _worker;
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public Contract Contract { get; private set; }
        public ICommand SearchCommand { get; }

        public string _specificationTailor;

        public string SpecificationTailor
        {
            get => _specificationTailor;
            set
            {
                _specificationTailor = value;
                OnPropertyChanged(nameof(SpecificationTailor));
            }
        }
        private double _budget;
        public double Budget
        {
            get => _budget;
            set
            {
                _budget = value;
                OnPropertyChanged(nameof(Budget));
            }
        }

        private string _workerRole;
        public string WorkerRole
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(WorkerRole));
            }
        }

        public UpdateContractViewModel(Contract contract)
        {
            // Assuming worker is fetched based on the user ID
            _worker = new Worker(UserSession.Current.UserId);
            WorkerRole = _worker.Role;

            // Populate the data based on the role
            Contract = contract;

            // SearchCommand setup (just for example)
            SearchCommand = new RelayCommand<string>(Search);
        }
        private void Search(object obj)
        {
            // Implement search logic based on some criteria (for demonstration purposes)
            MessageBox.Show("Search logic here", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
   
}
