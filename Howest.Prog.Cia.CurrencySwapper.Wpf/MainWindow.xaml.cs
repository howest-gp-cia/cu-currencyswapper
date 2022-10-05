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

namespace Howest.Prog.Cia.CurrencySwapper.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double EurToUsdRate = 0.98913127; // op 5 oktober 2022

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtInput.Text;

            if (!double.TryParse(userInput, out double amount) || amount < 0)
            {
                MessageBox.Show("Please enter a positive amount", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                double targetAmount = amount * EurToUsdRate;

                txtOutput.Text = $"{amount} EUR = {targetAmount:N2} USD";
            }
        }
    }
}
