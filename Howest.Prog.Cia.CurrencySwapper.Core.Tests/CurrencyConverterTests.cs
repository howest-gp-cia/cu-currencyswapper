using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using System;
using Xunit;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Tests
{

    public class CurrencyConverterTests
    {
        [Fact]
        public void Convert_RateGreaterThenZero_ReturnsProductOfAmountAndRate()
        {
            //arrange
            var converter = new CurrencyConverter();
            double amount = 2.5;
            double rate = 2.0;
            double expectedResult = 5.0;

            //act
            double result = converter.Convert(amount, rate);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-12.345)]
        [InlineData(double.MinValue)]
        public void Convert_RateLessOrEqualToZero_ThrowsException(double rate)
        {
            //arrange
            var converter = new CurrencyConverter();
            var amount = 2.5;

            //act
            Action conversion = new Action(() => converter.Convert(amount, rate));

            //assert
            Assert.ThrowsAny<Exception>(conversion);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-12.345)]
        [InlineData(double.MinValue)]
        public void Convert_RateLessOrEqualToZero_ThrowsArgumentExceptionWithParamNameRate(double rate)
        {
            //arrange
            var converter = new CurrencyConverter();
            var amount = 2.5;
            var expectedParamName = "rate";

            //act
            Action conversion = new Action(() => converter.Convert(amount, rate));

            //assert
            var exception = Assert.Throws<ArgumentException>(conversion);
            Assert.Equal(expectedParamName, exception.ParamName);
        }
    }
}
