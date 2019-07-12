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
        public static int Convert(string romanNumeral)
        {
            var runningTotal = 0;
            for (int i = 0; i < romanNumeral.Length; i++)
            {
                var currentCharacterValue = _characterValues[romanNumeral[i]];
                bool isDeductionPair = false;
                if (i < romanNumeral.Length - 1)
                {
                    var nextCharacterValue = _characterValues[romanNumeral[i + 1]];
                    isDeductionPair = currentCharacterValue < nextCharacterValue;
                    if (isDeductionPair)
                    {
                        if (currentCharacterValue == 1 && (nextCharacterValue!= 5 && nextCharacterValue!=10))
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