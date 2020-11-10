using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;
using Moq;
using System;
using Xunit;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Tests
{

    public class CurrencyConverterTests
    {
        [Fact]
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

        [Theory]
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

        [Theory]
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

        [Fact]
        public void Convert_ConvertibleCurrencies_ReturnsProductOfAmountAndRate() //testing Convert(double, string, string)
        {
            //arrange
            double amount = 2.5;

            var rate = new Rate
            {
                FromCurrency = "AAA",
                ToCurrency = "BBB",
                ExchangeRate = 2.0
            };

            double expectedResult = 5.0;

            var rateService = new Mock<IRateService>();
            rateService
                .Setup(m => m.CanConvertBetween(rate.FromCurrency, rate.ToCurrency))
                .Returns(true);

            rateService
                .Setup(m => m.GetRate(rate.FromCurrency, rate.ToCurrency))
                .Returns(rate);

            var converter = new CurrencyConverter(rateService.Object);

            //act
            double result = converter.Convert(amount, rate.FromCurrency, rate.ToCurrency);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Convert_CallsIRateServiceGetRate() //testing Convert(double, string, string)
        {
            //arrange
            double amount = 2.5;
            var rate = new Rate
            {
                FromCurrency = "AAA",
                ToCurrency = "BBB",
                ExchangeRate = 2.0
            };

            var rateService = new Mock<IRateService>();
            rateService
                .Setup(m => m.CanConvertBetween(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            rateService
               .Setup(m => m.GetRate(It.IsAny<string>(), It.IsAny<string>()))
               .Returns(rate);

            var converter = new CurrencyConverter(rateService.Object);

            //act
            converter.Convert(amount, rate.FromCurrency, rate.ToCurrency);

            //assert
            rateService.Verify(mock => mock.GetRate(rate.FromCurrency, rate.ToCurrency));
        }

        [Fact]
        public void Convert_InconvertibleCurrencies_NotSupportedException() //testing Convert(double, string, string)
        {
            //arrange
            double amount = 2.5;
            var rate = new Rate
            {
                FromCurrency = "AAA",
                ToCurrency = "BBB",
                ExchangeRate = 2.0
            };

            var rateService = new Mock<IRateService>();
            rateService
                .Setup(m => m.CanConvertBetween(rate.FromCurrency, rate.ToCurrency))
                .Returns(false);

            var converter = new CurrencyConverter(rateService.Object);

            //act
            Action conversion = new Action(() => converter.Convert(amount, rate.FromCurrency, rate.ToCurrency));

            //assert
            Assert.Throws<NotSupportedException>(conversion);
        }

    }
}
