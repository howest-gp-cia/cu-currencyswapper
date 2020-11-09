using Howest.Prog.Cia.UnitConverter.Core;
using System;

namespace Howest.Prog.Cia.CurrencySwapper.Cons
{
    class Program
    {
        private const double EurToUsdRate = 1.189421; // op 9 november 2020

        static void Main(string[] args)
        {
            AmountValidator validator = new AmountValidator();
            CurrencyConverter converter = new CurrencyConverter();

            Console.WriteLine("Currency Converter\n==============");
            Console.WriteLine("-- enter non-number to exit. --\n");
            bool numberEntered;

            do
            {
                Console.Write("Enter amount (EUR): ");
                string userInput = Console.ReadLine();
                numberEntered = double.TryParse(userInput, out double amount);
                if (numberEntered)
                {
                    var validationResult = validator.Validate(amount);
                    if (validationResult.IsValid)
                    {
                        double convertedAmount = converter.Convert(amount, EurToUsdRate);

                        Console.WriteLine($"{amount} EUR = {convertedAmount:N2} USD");
                    }
                    else
                    {
                        ShowError(validationResult.ErrorMessage);
                    }
                }
            } while (numberEntered);
        }

        static void ShowError(string errorMessage)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ForegroundColor = currentColor;
        }
    }
}
