namespace Howest.Prog.Cia.UnitConverter.Core
{
    public class AmountValidator
    {
        private const string AmountMustBePositive = "Amount must be a positive number";

        public ValidationResult Validate(double amount)
        {
            var result = new ValidationResult();

            if (amount < 0)
            {
                result.ErrorMessage = AmountMustBePositive;
            }
            else
            {
                result.IsValid = true;
            }
            return result;
        }
    }
}
