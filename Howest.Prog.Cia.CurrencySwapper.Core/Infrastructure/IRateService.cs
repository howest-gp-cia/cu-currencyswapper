using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure
{
    public interface IRateService
    {
        Rate GetRate(string fromCurrency, string toCurrency);

        IEnumerable<string> GetSupportedCurrencies();
    }
}
