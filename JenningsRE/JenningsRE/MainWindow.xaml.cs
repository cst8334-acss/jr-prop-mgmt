﻿using System;
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

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void switchScreen(object sender, RoutedEventArgs e)
        {
            AnalysisWindow Analysis = new AnalysisWindow();
            Analysis.Show();
            Close();
        }

        private void AddTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTenant Tenant = new AddTenant();
            Tenant.Show();
        }
    }
}
