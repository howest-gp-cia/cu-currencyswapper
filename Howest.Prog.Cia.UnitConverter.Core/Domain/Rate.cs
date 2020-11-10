using System;
using System.Collections.Generic;
using System.Text;

namespace Howest.Prog.Cia.UnitConverter.Core.Domain
{
    public class Rate
    {
        public double ExchangeRate { get; set; }

        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }
    }
}
