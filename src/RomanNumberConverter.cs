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

        private static readonly Dictionary<int, string> _romanEquivalents = new Dictionary<int, string>{
            { 1, "I"},
            { 5, "V"},
            { 10, "X"},
            { 50, "L"},
            { 100, "C"},
            { 500, "D"},
            { 1000, "M"},
            { 4, "IV"},
            { 9, "IX"},
            { 40, "XL"},
            { 90, "XC"},
            { 400, "CD"},
            { 900, "CM"}
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
            if (_romanEquivalents.ContainsKey(decimalNumber))
            {
                return _romanEquivalents[decimalNumber];
            }
            else
            {
                var thousands = decimalNumber / 1000;
                if (thousands > 0)
                {
                    return new String('M', thousands) + ConvertToRomanNumerals(decimalNumber - 1000 * thousands);
                }
                if (decimalNumber / 900 == 1)
                {
                    return "CM" + ConvertToRomanNumerals(decimalNumber - 900);
                }
                if (decimalNumber / 500 == 1)
                {
                    return "D" + ConvertToRomanNumerals(decimalNumber - 500);
                }
                if (decimalNumber / 400 == 1)
                {
                    return "CD" + ConvertToRomanNumerals(decimalNumber - 400);
                }
                var hundreds = decimalNumber / 100;
                if (hundreds > 0)
                {
                    return new String('C', hundreds) + ConvertToRomanNumerals(decimalNumber - 100 * hundreds);
                }
                if (decimalNumber / 90 == 1)
                {
                    return "XC" + ConvertToRomanNumerals(decimalNumber - 90);                    
                }
                if (decimalNumber / 50 == 1)
                {
                    return "L" + ConvertToRomanNumerals(decimalNumber - 50);                    
                }
                if (decimalNumber / 40 == 1)
                {
                    return "XL" + ConvertToRomanNumerals(decimalNumber - 40);                    
                }
                var tens = decimalNumber /10;
                if (tens > 0)
                {
                    return new String('X', tens) + ConvertToRomanNumerals(decimalNumber - 10 * tens);                    
                }
                return null;
            }
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