﻿using Interface.ViewModels;
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
    /// <summary>
    /// Interaction logic for DashboardCustomer.xaml
    /// </summary>
    public partial class DashboardCustomer : Page
    {
        public DashboardCustomer()
        {
            InitializeComponent();
            DataContext = new DashboardCustomerViewModel();
        }
    }
}
