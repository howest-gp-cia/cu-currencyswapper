using Howest.Prog.Cia.CurrencySwapper.Core;
using Howest.Prog.Cia.CurrencySwapper.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Howest.Prog.Cia.CurrencySwapper.Web.Controllers
{
    public class CurrencyController : Controller
    {
        private const double EurToUsdRate = 0.98913127; // op 5 oktober 2022
        private readonly AmountValidator _validator;
        private readonly CurrencyConverter _converter;

        public CurrencyController(AmountValidator validator, CurrencyConverter converter)
        {
            _validator = validator;
            _converter = converter;
        }

        public IActionResult Convert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Convert(ConvertViewModel model)
        {
            double amount = model.Amount;

            var validationResult = _validator.Validate(amount);
            if (validationResult.IsValid)
            {
                double convertedAmount = _converter.Convert(amount, EurToUsdRate);
                model.ConvertedAmount = convertedAmount;
                model.ShowResult = true;
                return View(model);
            }
            else
            {
                ModelState.AddModelError(nameof(ConvertViewModel.Amount), validationResult.ErrorMessage);
                return View(model);
            }
        }

    }
}
