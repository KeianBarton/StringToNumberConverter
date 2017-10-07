using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringToNumberConverter
{
    public static class ConverterHelper
    {
        internal static double Convert(string stringValue)
        {
            var splitString = SplitNumberString(stringValue);
            var multipliersArray = CalculateMultipliersOfSplitString(splitString);
            var value = CalculateNumberValue(multipliersArray);
            return value;
        }

        internal static double CalculateNumberValue(int[] multipliersArray)
        {
            double value = 0;
            value += multipliersArray[0] * Math.Pow(10, 9);
            value += multipliersArray[1] * Math.Pow(10, 6);
            value += multipliersArray[2] * Math.Pow(10, 3);
            value += multipliersArray[3];
            value *= multipliersArray[4]; // sign
            return value;
        }

        internal static int[] CalculateMultipliersOfSplitString(string[] splitString)
        {
            var multipliersArray = new int[5];
            multipliersArray[0] = ConvertNumberSmallerThanThousand(splitString[0]);
            multipliersArray[1] = ConvertNumberSmallerThanThousand(splitString[1]);
            multipliersArray[2] = ConvertNumberSmallerThanThousand(splitString[2]);
            multipliersArray[3] = ConvertNumberSmallerThanThousand(splitString[3]);
            multipliersArray[4] = FindIfNumberStringIsNegative(splitString);
            return multipliersArray;
        }

        private static int FindIfNumberStringIsNegative(string[] splitString)
        {
            foreach(var str in splitString)
            {
                if (str == null) continue;
                if (str.Contains("negative") ||
                    str.Contains("minus"))
                return -1;
            }
            return 1;
        }

        internal static string[] SplitNumberString(string stringValue)
        {
            if (stringValue == null) return new string[] { "" };

            var stringWithoutCommas = stringValue.Replace(",", String.Empty).ToLower() ;

            var splitString = stringWithoutCommas.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            var indexOfBillion = CalculateIndexOfBillion(splitString);
            var indexOfMillion = CalculateIndexOfMillion(splitString);
            var indexOfLastThousand = CalculateIndexOfLastThousand(splitString);

            return CombineNumberStringByComponents(splitString, indexOfBillion, indexOfMillion, indexOfLastThousand);
        }

        internal static string[] CombineNumberStringByComponents(
            string[] splitString, int indexOfBillion, int indexOfMillion, int indexOfLastThousand)
        {
            if (splitString == null) return new string[4];

            var i = 0;
            var combinedStringArray = new string[4];
            for (; i < indexOfBillion + 1; i++)
            {
                combinedStringArray[0] += splitString[i] + " ";
            }
            if (combinedStringArray[0] != null)
                combinedStringArray[0] = combinedStringArray[0].Replace(
                " " + NumberWordDescriptions.billion.ToString() + " ", String.Empty);
            for (; i < indexOfMillion + 1; i++)
            {
                combinedStringArray[1] += splitString[i] + " ";
            }
            if (combinedStringArray[1] != null)
                combinedStringArray[1] = combinedStringArray[1].Replace(
                " " + NumberWordDescriptions.million.ToString() + " ", String.Empty);
            for (; i < indexOfLastThousand + 1; i++)
            {
                combinedStringArray[2] += splitString[i] + " ";
            }
            if (combinedStringArray[2] != null)
                combinedStringArray[2] = combinedStringArray[2].Replace(
                " " + NumberWordDescriptions.thousand.ToString() + " ", String.Empty);
            for (; i < splitString.Length; i++)
            {
                combinedStringArray[3] += splitString[i] + " ";
            }
            if (combinedStringArray[3] != null)
                combinedStringArray[3] = combinedStringArray[3].TrimEnd(new char[0]);
            return combinedStringArray;
        }

        internal static int ConvertNumberSmallerThanThousand(string stringValue)
        {
            if (String.IsNullOrWhiteSpace(stringValue)) return 0;

            int value = 0;
            var indexOfHundred = stringValue.IndexOf(NumberWordDescriptions.hundred.ToString());

            if (indexOfHundred >= 0)
            {
                var stringBeforeHundredWithSpaces = stringValue.Remove(indexOfHundred);
                var stringBeforeHundredWithoutSpaces = stringBeforeHundredWithSpaces.Replace(" ",String.Empty);
                value += 100 * (int)Enum.Parse(typeof(NumberWordDescriptions), stringBeforeHundredWithoutSpaces);

                var stringAfterHundred = stringValue.Substring(indexOfHundred + NumberWordDescriptions.hundred.ToString().Length);

                foreach (var word in Enum.GetNames(typeof(NumberWordDescriptions)))
                {
                    if(NumberWordIsKnown(word, stringAfterHundred))
                        value += (int)Enum.Parse(typeof(NumberWordDescriptions), word);
                }
            }
            else
            {
                foreach (var word in Enum.GetNames(typeof(NumberWordDescriptions)))
                {

                    if (NumberWordIsKnown(word, stringValue))
                        value += (int)Enum.Parse(typeof(NumberWordDescriptions), word);
                }
            }

            return value;
        }

        private static bool NumberWordIsKnown(string wordToCheck, string numberString)
        {
            // e.g. "SEVENty" and "seven ..." and "... seven" cases covered
            if (numberString.Contains(wordToCheck.ToString() + " ") || (numberString.EndsWith(wordToCheck)))
                return true;
            else
                return false;
        }

        internal static int CalculateIndexOfBillion(string[] splitString)
        {
            for(var i = 0; i < splitString.Length; i++)
            {
                if (String.Equals(
                    splitString[i],
                    NumberWordDescriptions.billion.ToString(),
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        internal static int CalculateIndexOfMillion(string[] splitString)
        {
            for (var i = 0; i < splitString.Length; i++)
            {
                if (String.Equals(
                    splitString[i],
                    NumberWordDescriptions.million.ToString(),
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        internal static int CalculateIndexOfLastThousand(string[] splitString)
        {
            int lastIndex = -1;
            for (var i = 0; i < splitString.Length; i++)
            {
                if (String.Equals(
                    splitString[i],
                    NumberWordDescriptions.thousand.ToString(),
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    lastIndex = i;
                }
            }
            return lastIndex;
        }
    }
}
