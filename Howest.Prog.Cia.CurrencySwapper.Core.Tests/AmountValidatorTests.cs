using Xunit;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Tests
{
    public class AmountValidatorTests
    {
        [Theory]
        [InlineData(0.0)]
        [InlineData(12.345)]
        [InlineData(double.MaxValue)]
        public void Validate_AmountGreaterOrEqualToZero_ReturnsResultWithValidTrue(double amount)
        {
            //arrange
            var validator = new AmountValidator();

            //act
            var result = validator.Validate(amount);

            //assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(-0.01)]
        [InlineData(-12.345)]
        [InlineData(double.MinValue)]
        public void Validate_AmountLessThanZero_ReturnsResultWithValidFalse(double amount)
        {
            //arrange
            var validator = new AmountValidator();

            //act
            var result = validator.Validate(amount);

            //assert
            Assert.False(result.IsValid);
        }


        [Theory]
        [InlineData(-0.01)]
        [InlineData(-12.345)]
        [InlineData(double.MinValue)]
        public void Validate_AmountLessThanZero_ReturnsResultWithErrorMessage(double amount)
        {
            //arrange
            var validator = new AmountValidator();
            var expectedError = AmountValidator.AmountMustBePositive;

            //act
            var result = validator.Validate(amount);

            //assert
            Assert.Equal(expectedError, result.ErrorMessage);
        }
    }
}
