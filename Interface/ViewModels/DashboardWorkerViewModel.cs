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

        public DashboardWorkerViewModel(string userId)
        {
            WorkerName = $"Hello, {UserSession.Current.Name}!";
            Worker = new Worker(userId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
