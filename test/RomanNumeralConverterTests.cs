using System;
using Xunit;

namespace CraftingCode
{
    public class RomanNumeralConverterTests
    {
        [Theory]
        [InlineData("I", 1)]
        [InlineData("V", 5)]
        [InlineData("X", 10)]
        [InlineData("L", 50)]
        [InlineData("C", 100)]
        [InlineData("D", 500)]
        [InlineData("M", 1000)]
        public void SingleCharacterTests(string romanNumeral, int decimalEquivalent)
        {
            var actual = RomanNumeralConverter.Convert(romanNumeral);
            Assert.Equal(decimalEquivalent, actual);
        }

        [Theory]
        [InlineData("II", 2)]
        [InlineData("XX", 20)]
        [InlineData("CC", 200)]
        [InlineData("MM", 2000)]
        [InlineData("MMC", 2100)]
        public void MultipleCharacterTests(string romanNumeral, int decimalEquivalent)
        {
            var actual = RomanNumeralConverter.Convert(romanNumeral);
            Assert.Equal(decimalEquivalent, actual);
        }

        [Theory]
        [InlineData("IV", 4)]
        [InlineData("IX", 9)]
        [InlineData("XL", 40)]
        [InlineData("XC", 90)]
        [InlineData("CM", 900)]
        public void SmallerNumberDeductsFromSubsequentLargerNumber(string romanNumeral, int decimalEquivalent)
        {
            var actual = RomanNumeralConverter.Convert(romanNumeral);
            Assert.Equal(decimalEquivalent, actual);
        }

        [Theory]
        [InlineData("MMCDXCIX", 2499)]
        [InlineData("MMMCMXLIX", 3949)]
        public void TheComplexCases(string romanNumeral, int decimalEquivalent)
        {
            var actual = RomanNumeralConverter.Convert(romanNumeral);
            Assert.Equal(decimalEquivalent, actual);
        }
        [Theory]
        [InlineData("IL")]
        [InlineData("IC")]
        [InlineData("ID")]
        [InlineData("IM")]
        public void IllegalLowerNumbersThrowExceptionForI(string romanNumeral)
        {
            Assert.Throws<ArgumentException>(() => RomanNumeralConverter.Convert(romanNumeral));
        }

        [Theory]
        [InlineData("VX")]
        [InlineData("VL")]
        [InlineData("VC")]
        [InlineData("VD")]
        [InlineData("VVMD")]
        public void VIsAlwaysAnIllegalLowerNumber(string romanNumeral)
        {
            Assert.Throws<ArgumentException>(() => RomanNumeralConverter.Convert(romanNumeral));
        }

        [Theory]
        [InlineData("XD")]
        [InlineData("XM")]
        public void IllegalLowerNumbersThrowExceptionForX(string romanNumeral)
        {
            Assert.Throws<ArgumentException>(() => RomanNumeralConverter.Convert(romanNumeral));
        }
    }

}
