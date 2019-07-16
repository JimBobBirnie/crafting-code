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
    }
}