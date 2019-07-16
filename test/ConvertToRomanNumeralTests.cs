using System;
using Xunit;

namespace CraftingCode
{
    public class ConvertToRomanNumeralTests
    {
        [Theory]
        [InlineData(1, "I")]
        [InlineData(5, "V")]
        [InlineData(10, "X")]
        [InlineData(50, "L")]
        [InlineData(100, "C")]
        [InlineData(500, "D")]
        [InlineData(1000, "M")]
        public void ShouldConvertSingleLetterEquivalents(int decimalNumber, string expectedRomanNueral)
        {
            var actual = RomanNumeralConverter.ConvertToRomanNumerals(decimalNumber);
            Assert.Equal(expectedRomanNueral, actual);
        }

        [Fact]
        public void ThrowIfDecimalExceedsMaximum()
        {
            Assert.Throws<ArgumentException>(() => RomanNumeralConverter.ConvertToRomanNumerals(4000));
        }

        [Theory]
        [InlineData(4, "IV")]
        [InlineData(9, "IX")]
        [InlineData(40, "XL")]
        [InlineData(90, "XC")]
        [InlineData(400, "CD")]
        [InlineData(900, "CM")]
        public void DeductionCharactersWorkCorrectly(int decimalNumber, string expectedRomanNumeral)
        {
            var actual = RomanNumeralConverter.ConvertToRomanNumerals(decimalNumber);
            Assert.Equal(expectedRomanNumeral, actual);
        }

        [Theory]
        [InlineData(3001, "MMMI")]
        [InlineData(901, "CMI")]
        [InlineData(910, "CMX")]
        [InlineData(550, "DL")]
        public void LeftToRightCases(int decimalNumber, string expectedRomanNumeral)
        {
            var actual = RomanNumeralConverter.ConvertToRomanNumerals(decimalNumber);
            Assert.Equal(expectedRomanNumeral, actual);
        }
    }
}