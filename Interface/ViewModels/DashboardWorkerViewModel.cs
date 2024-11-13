using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels
{
    internal class DashboardWorkerViewModel : INotifyPropertyChanged
    {
        private Worker _worker;
        private DatabaseHelper dbHelper = new DatabaseHelper();

        public Worker Worker
        {
            get { return _worker; }
            set
            {
                _worker = value;
                OnPropertyChanged(nameof(Worker));
            }
        }

        public DashboardWorkerViewModel(string userId)
        {
            Worker = new Worker(userId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
