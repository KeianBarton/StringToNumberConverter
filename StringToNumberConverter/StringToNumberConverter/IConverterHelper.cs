namespace StringToNumberConverter
{
    public interface IConverterHelper
    {
        double Convert(string stringValue);

        double CalculateNumberValue(int[] multipliersArray);

        int[] CalculateMultipliersOfSplitString(string[] splitString);

        string[] SplitNumberString(string stringValue);

        int ConvertNumberSmallerThanThousand(string stringValue);

        int CalculateIndexOfBillion(string[] splitString);

        int CalculateIndexOfMillion(string[] splitString);

        int CalculateIndexOfLastThousand(string[] splitString);

    }
}