using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;
using Howest.Prog.Cia.CurrencySwapper.Infrastructure.CurrConv;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Howest.Prog.Cia.CurrencySwapper.Infrastructure.Realtime
{
    /// <summary>
    /// Queries the online ExchangeRatesApi which provides currency rates published by the European Central Bank
    /// </summary>
    /// <remarks>
    /// View documentation here https://exchangeratesapi.io/ 
    /// </remarks>
    public class ExchangeRatesApiClient : IRateService
    {
        private string ApiConvertUrl = "https://api.exchangeratesapi.io/latest?base={0}&symbols={1}";
        private string ApiCurrenciesUrl = "https://api.exchangeratesapi.io/latest";

        public ExchangeRatesApiClient()
        {

        }

        public Rate GetRate(string fromCurrency, string toCurrency)
        {
            double rate = 0;
            string url = string.Format(ApiConvertUrl, fromCurrency, toCurrency);
            string json = ReadJsonFromUrl(url);

            using (var document = JsonDocument.Parse(json))
            {
                rate = document.RootElement.GetProperty("rates").GetProperty(toCurrency).GetDouble();
            }

            return new Rate
            {
                ExchangeRate = rate,
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency
            };
        }

        public IEnumerable<string> GetSupportedCurrencies()
        {
            string url = string.Format(ApiCurrenciesUrl);
            string json = ReadJsonFromUrl(url);
            var dto = JsonSerializer.Deserialize<ResultsDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var supportedCurrencies = dto.Rates.Select(result => result.Key).ToList();
            supportedCurrencies.Add("EUR");
            return supportedCurrencies.OrderBy(code => code);
        }

        public bool CanConvertBetween(string fromCurrency, string toCurrency)
        {
            var currencies = GetSupportedCurrencies();
            return currencies.Any(c => c == fromCurrency || c == toCurrency);
        }

        protected string ReadJsonFromUrl(string url)
        {
            Task<string> webTask = Task.Run(() =>
            {
                using (var client = new HttpClient())
                {

                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync();
                    }
                }
                return null;
            });

            return webTask.Result;
        }
    }
}
