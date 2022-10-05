using System;

namespace Howest.Prog.Cia.CurrencySwapper.Cons
{
    class Program
    {
        private const double EurToUsdRate = 0.98913127; // op 5 oktober 2022

        static void Main(string[] args)
        {
            Console.WriteLine("Currency Converter\n==============");
            Console.Write("Enter amount (EUR): ");
            string userInput = Console.ReadLine();

            double amount;
            if (!double.TryParse(userInput, out amount) || amount < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter a positive amount!");
            }
            else
            {
                double targetAmount = amount * EurToUsdRate;

                Console.WriteLine($"{amount} EUR = {targetAmount:N2} USD");
            }
        }
    }
}
