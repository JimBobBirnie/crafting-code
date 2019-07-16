using System;
using System.Collections.Generic;

namespace CraftingCode
{
    public class RomanNumeralConverter
    {
        private static readonly Dictionary<char, int> _decimalEquivalents = new Dictionary<char, int>{
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        private static readonly Dictionary<int, string> _orderedRomanEquivalents = new Dictionary<int, string>{

            { 1000, "M"},
            { 900, "CM"},
            { 500, "D"},
            { 400, "CD"},
            { 100, "C"},
            { 90, "XC"},
            { 50, "L"},
            { 40, "XL"},
            { 10, "X"},
            { 9, "IX"},
            { 5, "V"},
            { 4, "IV"},
            { 1, "I"}
        };


        private static readonly Dictionary<char, List<char>> _allowedDeductionPairs = new Dictionary<char, List<char>>{
            {'I', new List<char>{'V','X'}},
            {'X', new List<char>{'L', 'C'}},
            {'C', new List<char>{'D', 'M'}}
        };

        private static bool IsValidDeductionTriple(Nullable<char> previousCharacter, char currentCharacter, char nextCharacter)
        {

            return (!previousCharacter.HasValue || _decimalEquivalents[previousCharacter.Value] >=
                _decimalEquivalents[nextCharacter]) && _allowedDeductionPairs.ContainsKey(currentCharacter) &&
                _allowedDeductionPairs[currentCharacter].Contains(nextCharacter);
        }


        public static string ConvertToRomanNumerals(int decimalNumber)
        {
            if (decimalNumber >= 4000)
            {
                throw new ArgumentException("decimalNumber");
            }
            foreach (KeyValuePair<int, string> conversionPair in _orderedRomanEquivalents)
            {
                var units = decimalNumber / conversionPair.Key;
                if (units > 0)
                {
                    if (conversionPair.Value.Length == 1)
                    {
                        return new String(conversionPair.Value[0], units) +
                            ConvertToRomanNumerals(decimalNumber - conversionPair.Key * units);
                    }
                    else return conversionPair.Value + ConvertToRomanNumerals(decimalNumber - conversionPair.Key * units);
                }
            }
            return string.Empty;
        }

        public static int ConvertToDecimal(string romanNumeral)
        {
            var runningTotal = 0;
            for (int i = 0; i < romanNumeral.Length; i++)
            {
                var currentCharacterValue = _decimalEquivalents[romanNumeral[i]];
                var nextCharacterValue = 0;
                bool isDeductionPair = i < romanNumeral.Length - 1 &&
                    currentCharacterValue < (nextCharacterValue = _decimalEquivalents[romanNumeral[i + 1]]);
                if (isDeductionPair)
                {
                    var previousCharacter = i > 0 ? romanNumeral[i - 1] : (Nullable<char>)null;
                    if (!IsValidDeductionTriple(previousCharacter, romanNumeral[i], romanNumeral[i + 1]))
                    {
                        throw new ArgumentException("romanNumeral");
                    }
                    runningTotal += (nextCharacterValue - currentCharacterValue);
                    i++;
                }
                else
                {
                    runningTotal += currentCharacterValue;
                }
            }
            return runningTotal;
        }
    }
}