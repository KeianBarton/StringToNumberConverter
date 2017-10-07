using NUnit.Framework;
using Moq;
using StringToNumberConverter;
using System;

namespace StringToNumberConverterTests
{
    [TestFixture]
    public class ConverterHelperTests
    {
        [Test]
        public void ConvertNumberSmallerThanThousandTests()
        {
            // Arrange
            var zeroStr = "zero";
            var thirteenStr = "thirteen";
            var oneHundredStr = "one hundred";
            var twoHundredStr = "two hundred";
            var threeHundredAndNinetyNineStr = "three hundred and ninety nine";
            var fourHundredAndFiftySevenStr = "four hundred & fifty seven";
            var nineHundredAndFortyStr = "nine hundred & forty";

            // Act
            var unknownVal = ConverterHelper.ConvertNumberSmallerThanThousand(String.Empty);
            var zeroVal = ConverterHelper.ConvertNumberSmallerThanThousand(zeroStr);
            var thirteenVal = ConverterHelper.ConvertNumberSmallerThanThousand(thirteenStr);
            var oneHundredVal = ConverterHelper.ConvertNumberSmallerThanThousand(oneHundredStr);
            var twoHundredVal = ConverterHelper.ConvertNumberSmallerThanThousand(twoHundredStr);
            var threeHundredAndNinetyNineVal = ConverterHelper.ConvertNumberSmallerThanThousand(threeHundredAndNinetyNineStr);
            var fourHundredAndFiftySevenVal = ConverterHelper.ConvertNumberSmallerThanThousand(fourHundredAndFiftySevenStr);
            var nineHundredAndFortyVal = ConverterHelper.ConvertNumberSmallerThanThousand(nineHundredAndFortyStr);

            // Assert
            Assert.AreEqual(0, unknownVal);
            Assert.AreEqual(0, zeroVal);
            Assert.AreEqual(13, thirteenVal);
            Assert.AreEqual(100, oneHundredVal);
            Assert.AreEqual(200, twoHundredVal);
            Assert.AreEqual(399, threeHundredAndNinetyNineVal);
            Assert.AreEqual(457, fourHundredAndFiftySevenVal);
            Assert.AreEqual(940, nineHundredAndFortyVal);
        }

        [Test]
        public void CalculateIndexOfBillionTests()
        {
            // Arrange

            // Act
            var secondWord = ConverterHelper.CalculateIndexOfBillion(new string[] { "one", "billion" });
            var notFound = ConverterHelper.CalculateIndexOfBillion(new string[] { "one", "hundred", "thousand" });

            // Assert
            Assert.AreEqual(1, secondWord);
            Assert.AreEqual(-1, notFound);
        }

        [Test]
        public void CalculateIndexOfMillionTests()
        {
            // Arrange

            // Act
            var secondWord = ConverterHelper.CalculateIndexOfMillion(new string[] { "one", "million" });
            var notFound = ConverterHelper.CalculateIndexOfMillion(new string[] { "one", "hundred", "thousand" });

            // Assert
            Assert.AreEqual(1, secondWord);
            Assert.AreEqual(-1, notFound);
        }

        [Test]
        public void CalculateIndexOfLastThousandTests()
        {
            // Arrange

            // Act
            var seventhWord = ConverterHelper.CalculateIndexOfLastThousand(
                new string[] { "one", "million", "one", "hundred", "and", "twenty", "thousand", "and", "two" });
            var notFound = ConverterHelper.CalculateIndexOfLastThousand(new string[] { "one", "hundred" });

            // Assert
            Assert.AreEqual(6, seventhWord);
            Assert.AreEqual(-1, notFound);
        }

        [Test]
        public void CombineNumberStringByComponentsTests()
        {
            // Arrange
            var stringArray1 = "one billion two million one hundred and twenty thousand and two".Split(
                new char[0], StringSplitOptions.RemoveEmptyEntries);
            var expectedArray1 = new string[] { "one", "two", "one hundred and twenty", "and two" };

            var stringArray2 = "two million one hundred and twenty thousand".Split(
                new char[0], StringSplitOptions.RemoveEmptyEntries);
            var expectedArray2 = new string[] { null, "two", "one hundred and twenty", null };

            var stringArray3 = "two million".Split(
                new char[0], StringSplitOptions.RemoveEmptyEntries);
            var expectedArray3 = new string[] { null, "two", null, null };

            var stringArray4 = new string[] { "nine hundred and ninety nine" };
            var expectedArray4 = new string[] { null, null, null, "nine hundred and ninety nine" };

            var expectedArray5 = new string[4];

            // Act
            var actualArray1 = ConverterHelper.CombineNumberStringByComponents(stringArray1, 1, 3, 8);
            var actualArray2 = ConverterHelper.CombineNumberStringByComponents(stringArray2, -1, 1, 6);
            var actualArray3 = ConverterHelper.CombineNumberStringByComponents(stringArray3, -1, 1, -1);
            var actualArray4 = ConverterHelper.CombineNumberStringByComponents(stringArray4, -1, -1, -1);
            var actualArray5 = ConverterHelper.CombineNumberStringByComponents(null, -1, -1, -1);

            // Assert
            Assert.AreEqual(expectedArray1, actualArray1);
            Assert.AreEqual(expectedArray2, actualArray2);
            Assert.AreEqual(expectedArray3, actualArray3);
            Assert.AreEqual(expectedArray4, actualArray4);
            Assert.AreEqual(expectedArray5, actualArray5);
        }

        [Test]
        public void SplitNumberStringTests()
        {
            // TODO
            // let's use mocking here to mock out dependencies
        }

        [Test]
        public void CalculateMultipliersOfSplitStringTests()
        {
            // TODO
            // let's use mocking here to mock out dependencies
        }

        [Test]
        public void ConvertTests()
        {
            // todo
            Assert.AreEqual(0D, ConverterHelper.Convert("zero"));
            Assert.AreEqual(0D, ConverterHelper.Convert("zero billion, zero million, zero thousand and zero"));
            Assert.AreEqual(1000D, ConverterHelper.Convert("one thousand"));
            Assert.AreEqual(9999999D, ConverterHelper.Convert(
                "nine million, nine hundred and ninety nine thousand, nine hundred"+
                "and ninety nine"));
            Assert.AreEqual(123D, ConverterHelper.Convert("one hundred, and twenty three"));
            Assert.AreEqual(-2873320999D, ConverterHelper.Convert(
                "Minus two billion, eight hundred and seventy three million, "+
                "three hundred and twenty thousand, nine hundred and ninety nine"));
        }

        [Test]
        public void CalculateNumberValueTests()
        {
            // Arrange
            var exampleMultipliers1 = new int[] { 3, 200, 320, 999, 1 };
            var expectedResult1 = 3200320999D;

            var exampleMultipliers2 = new int[] { 0, 0, 999, 0, -1 };
            var expectedResult2 = -999000D;

            // Act
            var actualResult1 = ConverterHelper.CalculateNumberValue(exampleMultipliers1);
            var actualResult2 = ConverterHelper.CalculateNumberValue(exampleMultipliers2);

            // Assert
            Assert.AreEqual(expectedResult1, actualResult1);
            Assert.AreEqual(expectedResult2, actualResult2);
        }
    }
}
