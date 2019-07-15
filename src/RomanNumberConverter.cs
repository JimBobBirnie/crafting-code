using System;
using System.Collections.Generic;

namespace CraftingCode
{
    public class RomanNumeralConverter
    {
        private static Dictionary<char, int> _characterValues = new Dictionary<char, int>{
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        private static Dictionary<char, List<char>> _allowedDeductionPairs = new Dictionary<char, List<char>>{
            {'I', new List<char>{'V','X'}},
            {'X', new List<char>{'L', 'C'}},
            {'C', new List<char>{'D', 'M'}}
        };



        private static bool IsValidDeductionPair(Nullable<char> previousCharacter, char currentCharacter, char nextCharacter)
        {

            return (!previousCharacter.HasValue || _characterValues[previousCharacter.Value] >=
                   _characterValues[nextCharacter]) && _allowedDeductionPairs.ContainsKey(currentCharacter) &&
               _allowedDeductionPairs[currentCharacter].Contains(nextCharacter);
        }
        public static int Convert(string romanNumeral)
        {
            var runningTotal = 0;
            for (int i = 0; i < romanNumeral.Length; i++)
            {
                var currentCharacterValue = _characterValues[romanNumeral[i]];
                var nextCharacterValue = 0;
                bool isDeductionPair = i < romanNumeral.Length - 1 &&
                    currentCharacterValue < (nextCharacterValue = _characterValues[romanNumeral[i + 1]]);
                if (isDeductionPair)
                {
                    Nullable<char> previousCharacter = i > 0 ? romanNumeral[i - 1] : (Nullable<char>)null;
                 
                    if (!IsValidDeductionPair(previousCharacter, romanNumeral[i], romanNumeral[i + 1]))
                    {
                        throw new ArgumentException("romanNumeral");
                    }

                    runningTotal += (nextCharacterValue - currentCharacterValue);
                    i++;
                }
                else
                {
                    runningTotal += _characterValues[romanNumeral[i]];
                }
            }
            return runningTotal;
        }

        private static int GetCharacterEquivalent(char singleCharacter)
        {
            return _characterValues[singleCharacter];
        }
    }
}