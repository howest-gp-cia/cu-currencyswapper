using System;

namespace Howest.Prog.Cia.UnitConverter.Core
{
    public class CurrencyConverter
    {
        private const string AmountMustBePositive = "Conversion rate must be greater than zero";
        
        public double Convert(double amount, double rate)
        {
            if (rate <= 0)   //domain rule, koers kan nooit negatief of nul zijn
            {
                throw new ArgumentException(AmountMustBePositive, nameof(rate));
            }
            else
            {
                return amount * rate;
            }
        }
    }
}
