using Howest.Prog.Cia.UnitConverter.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Howest.Prog.Cia.UnitConverter.Core.Infrastructure
{
    public interface IRateService
    {
        Task<Rate> GetRate(string fromCurrency, string toCurrency);

        Task<IEnumerable<string>> GetSupportedCurrencies();
    }
}
