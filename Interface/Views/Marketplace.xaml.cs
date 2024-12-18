﻿using Interface.Helpers;
using Interface.ViewModels;
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
    /// Interaction logic for Marketplace.xaml
    /// </summary>
    public partial class Marketplace : Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public Marketplace()
        {
            InitializeComponent();
            DataContext = new MarketplaceViewModel();
        }
    }
}
