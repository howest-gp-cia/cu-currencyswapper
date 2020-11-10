using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using Howest.Prog.Cia.CurrencySwapper.Core.Validation;
using System.Windows;

namespace Howest.Prog.Cia.CurrencySwapper.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double EurToUsdRate = 1.189421; // op 9 november 2020
        
        private AmountValidator validator;
        private CurrencyConverter converter;

        public MainWindow()
        {
            validator = new AmountValidator();
            converter = new CurrencyConverter();

            InitializeComponent();
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtInput.Text;

            if (double.TryParse(userInput, out double amount))
            {
                var validationResult = validator.Validate(amount);
                if (validationResult.IsValid)
                {
                    double convertedAmount = converter.Convert(amount, EurToUsdRate);

                    txtOutput.Text = $"{amount} EUR = {convertedAmount:N2} USD";
                }
                else
                {
                    ShowErrorDialog(validationResult.ErrorMessage);
                }
            }
            else
            {
                ShowErrorDialog($"Your input '{userInput}' must be a number");
            }
        }

        private void ShowErrorDialog(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
