using Howest.Prog.Cia.CurrencySwapper.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Howest.Prog.Cia.CurrencySwapper.Web.Controllers
{
    public class CurrencyController : Controller
    {
        private const double EurToUsdRate = 1.0813771; //1 EUR is altijd 1,08 USD

        public CurrencyController()
        {
        }

        public IActionResult Convert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Convert(ConvertViewModel model)
        {
            double amount = model.Amount;
            if (amount < 0)
            {
                ModelState.AddModelError(nameof(ConvertViewModel.Amount), "Please enter a positive amount");
                return View(model);
            }
            else
            {
                double convertedAmount = amount * EurToUsdRate;

                model.ConvertedAmount = convertedAmount;
                model.ShowResult = true;
                return View(model);
            }
        }

    }
}
