using System;
using System.Collections.Generic;

namespace CraftingCode
{
    public class RomanNumeralConverter
    {
        private static Dictionary<char, int> _decimalEquivalents = new Dictionary<char, int>{
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

         private static Dictionary<int, char> _romanEquivalents = new Dictionary<int, char>{
            { 1, 'I'},
            { 5, 'V'},
            { 10, 'X'},
            { 50, 'L'},
            { 100, 'C'},
            { 500, 'D'},
            { 1000, 'M'}
        };

        public static string ConvertToRomanNumerals(int decimalNumber)
        {
            switch (decimalNumber)
            {
                case 1:
                    return "I";
                case 5:
                    return "V";
                case 10:
                    return "X";
                case 50:
                    return "L";
                case 100:
                    return "C";
                case 500:
                    return "D";
                case 1000:
                    return "M";
                default:
                    return null;
            }
        }

        private static Dictionary<char, List<char>> _allowedDeductionPairs = new Dictionary<char, List<char>>{
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