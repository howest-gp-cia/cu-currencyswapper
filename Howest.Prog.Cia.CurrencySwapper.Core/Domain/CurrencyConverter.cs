using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;
using System;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Domain
{
    public class CurrencyConverter
    {
        public const string AmountMustBePositive = "Conversion rate must be greater than zero";
        private readonly IRateService _rateService;

        public CurrencyConverter(IRateService rateService)
        {
            _rateService = rateService;
        }

        public double Convert(double amount, string fromCurrency, string toCurrency)
        {
            Rate rate = _rateService.GetRate(fromCurrency, toCurrency);

            return Convert(amount, rate.ExchangeRate);
        }

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
