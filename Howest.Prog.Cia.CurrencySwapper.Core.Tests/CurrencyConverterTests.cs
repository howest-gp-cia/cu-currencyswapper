using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;
using Moq;
using System;
using Xunit;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Tests
{

    public class CurrencyConverterTests
    {
        [Fact(DisplayName = "Converting result with a valid rate multiplies amount with rate")]
        public void Convert_RateGreaterThenZero_ReturnsProductOfAmountAndRate() //testing Convert(double, double)
        {
            //arrange
            double amount = 2.5;
            double rate = 2.0;
            double expectedResult = 5.0;

            var rateService = new Mock<IRateService>();
            var converter = new CurrencyConverter(rateService.Object);

            //act
            double result = converter.Convert(amount, rate);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "Converting with a rate <= 0 throws Exception", Skip = "Skipped: already tested in Convert_RateLessOrEqualToZero_ThrowsArgumentExceptionWithParamNameRate")]
        [InlineData(0)]
        [InlineData(-12.345)]
        [InlineData(double.MinValue)]
        public void Convert_RateLessOrEqualToZero_ThrowsException(double rate) //testing Convert(double, double)
        {
            //arrange
            var amount = 2.5;

            var rateService = new Mock<IRateService>();
            var converter = new CurrencyConverter(rateService.Object);

            //act
            Action conversion = new Action(() => converter.Convert(amount, rate));

            //assert
            Assert.ThrowsAny<Exception>(conversion);
        }

        [Theory(DisplayName = "Converting with a rate <= 0 throws ArgumentException containing the parameter name")]
        [InlineData(0)]
        [InlineData(-12.345)]
        [InlineData(double.MinValue)]
        public void Convert_RateLessOrEqualToZero_ThrowsArgumentExceptionWithParamNameRate(double rate) //testing Convert(double, double)
        {
            //arrange
            var amount = 2.5;
            var expectedParamName = "rate";

            var rateService = new Mock<IRateService>();
            var converter = new CurrencyConverter(rateService.Object);

            //act
            Action conversion = new Action(() => converter.Convert(amount, rate));

            //assert
            var exception = Assert.Throws<ArgumentException>(conversion);
            Assert.Equal(expectedParamName, exception.ParamName);
        }

        [Fact(DisplayName = "Converting between known currencies returns correct foreign amount")]
        public void Convert_ConvertibleCurrencies_ReturnsProductOfAmountAndRate() //testing Convert(double, string, string)
        {
            //arrange

            //act

            //assert
            throw new NotImplementedException();
        }

        [Fact(DisplayName = "Currency Conversion uses IRateService.GetRate")]
        public void Convert_CallsIRateServiceGetRate() //testing Convert(double, string, string)
        {
            //arrange

            //act

            //assert
            throw new NotImplementedException();
        }

        [Fact(DisplayName = "Converting to or from an unknown currency throws NotSupportedException")]
        public void Convert_InconvertibleCurrencies_NotSupportedException() //testing Convert(double, string, string)
        {
            //arrange

            //act

            //assert
            throw new NotImplementedException();
        }

    }
}
