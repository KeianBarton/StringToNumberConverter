using NUnit.Framework;
using Moq;
using StringToNumberConverter;
using System;

namespace StringToNumberConverterTests
{
    [TestFixture]
    public class ConverterHelperTests
    {
        private ConverterHelper converterHelper;
        private Mock<ConverterHelper> mockConverterHelper;

        [SetUp]
        public void SetUpConverter()
        {
            converterHelper = new ConverterHelper();
            mockConverterHelper = new Mock<ConverterHelper>() { CallBase = true };
        }

        [TearDown]
        public void DestroyConverter()
        {
            converterHelper = null;
            mockConverterHelper = null;
        }

        [Test]
        public void ConvertNumberSmallerThanThousandTests()
        {
            // Arrange
            var zeroStr = "zero";
            var thirteenStr = "thirteen";
            var oneHundredStr1 = "one hundred";
            var oneHundredStr2 = "a hundred";
            var twoHundredStr = "two hundred";
            var threeHundredAndNinetyNineStr = "three hundred and ninety nine";
            var fourHundredAndFiftySevenStr = "four hundred & fifty seven";
            var nineHundredAndFortyStr = "nine hundred & forty";
            var seventyThree = "73";
            // we want to ignore minuses
            var minusTwentyThree1 = "-23";
            var minusTwentyThree2 = "minus 23";
            var minusTwentyThree3 = "negative twenty three";

            // Act
            var unknownVal = converterHelper.ConvertNumberSmallerThanThousand(String.Empty);
            var zeroVal = converterHelper.ConvertNumberSmallerThanThousand(zeroStr);
            var thirteenVal = converterHelper.ConvertNumberSmallerThanThousand(thirteenStr);
            var oneHundredVal1 = converterHelper.ConvertNumberSmallerThanThousand(oneHundredStr1);
            var oneHundredVal2 = converterHelper.ConvertNumberSmallerThanThousand(oneHundredStr2);
            var twoHundredVal = converterHelper.ConvertNumberSmallerThanThousand(twoHundredStr);
            var threeHundredAndNinetyNineVal = converterHelper.ConvertNumberSmallerThanThousand(threeHundredAndNinetyNineStr);
            var fourHundredAndFiftySevenVal = converterHelper.ConvertNumberSmallerThanThousand(fourHundredAndFiftySevenStr);
            var nineHundredAndFortyVal = converterHelper.ConvertNumberSmallerThanThousand(nineHundredAndFortyStr);
            var seventyThreeVal = converterHelper.ConvertNumberSmallerThanThousand(seventyThree);
            var minusTwentyThree1Val = converterHelper.ConvertNumberSmallerThanThousand(minusTwentyThree1);
            var minusTwentyThree2Val = converterHelper.ConvertNumberSmallerThanThousand(minusTwentyThree2);
            var minusTwentyThree3Val = converterHelper.ConvertNumberSmallerThanThousand(minusTwentyThree3);

            // Assert
            Assert.AreEqual(0, unknownVal);
            Assert.AreEqual(0, zeroVal);
            Assert.AreEqual(13, thirteenVal);
            Assert.AreEqual(100, oneHundredVal1);
            Assert.AreEqual(100, oneHundredVal2);
            Assert.AreEqual(200, twoHundredVal);
            Assert.AreEqual(399, threeHundredAndNinetyNineVal);
            Assert.AreEqual(457, fourHundredAndFiftySevenVal);
            Assert.AreEqual(940, nineHundredAndFortyVal);
            Assert.AreEqual(73, seventyThreeVal);
            Assert.AreEqual(23, minusTwentyThree1Val);
            Assert.AreEqual(23, minusTwentyThree2Val);
            Assert.AreEqual(23, minusTwentyThree3Val);
        }

        [Test]
        public void CalculateIndexOfBillionTests()
        {
            // Arrange

            // Act
            var secondWord = converterHelper.CalculateIndexOfBillion(new string[] { "one", "billion" });
            var notFound = converterHelper.CalculateIndexOfBillion(new string[] { "one", "hundred", "thousand" });

            // Assert
            Assert.AreEqual(1, secondWord);
            Assert.AreEqual(-1, notFound);
        }

        [Test]
        public void CalculateIndexOfMillionTests()
        {
            // Arrange

            // Act
            var secondWord = converterHelper.CalculateIndexOfMillion(new string[] { "one", "million" });
            var notFound = converterHelper.CalculateIndexOfMillion(new string[] { "one", "hundred", "thousand" });

            // Assert
            Assert.AreEqual(1, secondWord);
            Assert.AreEqual(-1, notFound);
        }

        [Test]
        public void CalculateIndexOfLastThousandTests()
        {
            // Arrange

            // Act
            var seventhWord = converterHelper.CalculateIndexOfLastThousand(
                new string[] { "one", "million", "one", "hundred", "and", "twenty", "thousand", "and", "two" });
            var notFound = converterHelper.CalculateIndexOfLastThousand(new string[] { "one", "hundred" });

            // Assert
            Assert.AreEqual(6, seventhWord);
            Assert.AreEqual(-1, notFound);
        }

        [Test]
        public void SplitNumberStringTests()
        {
            // Arrange
            var stringExample1 = "one billion two million one hundred and twenty thousand and two";
            var expectedArray1 = new string[] { "one", "two", "one hundred twenty", "two" };

            var stringExample2 = "two million one hundred and twenty thousand";
            var expectedArray2 = new string[] { null, "two", "one hundred twenty", null };

            var stringExample3 = "two million";
            var expectedArray3 = new string[] { null, "two", null, null };

            var stringExample4 = "nine hundred and ninety nine";
            var expectedArray4 = new string[] { null, null, null, "nine hundred ninety nine" };

            var expectedArray5 = new string[4];

            var stringExample6 = "zero billion, zero million, zero thousand and zero";
            var expectedArray6 = new string[] { "zero", "zero", "zero", "zero" };

            var stringExample7 = "zero";
            var expectedArray7 = new string[] { null, null, null, "zero" };

            // Act
            var actualArray1 = converterHelper.SplitNumberString(stringExample1);
            var actualArray2 = converterHelper.SplitNumberString(stringExample2);
            var actualArray3 = converterHelper.SplitNumberString(stringExample3);
            var actualArray4 = converterHelper.SplitNumberString(stringExample4);
            var actualArray5 = converterHelper.SplitNumberString(String.Empty);
            var actualArray6 = converterHelper.SplitNumberString(stringExample6);
            var actualArray7 = converterHelper.SplitNumberString(stringExample7);

            // Assert
            Assert.AreEqual(expectedArray1, actualArray1);
            Assert.AreEqual(expectedArray2, actualArray2);
            Assert.AreEqual(expectedArray3, actualArray3);
            Assert.AreEqual(expectedArray4, actualArray4);
            Assert.AreEqual(expectedArray5, actualArray5);
            Assert.AreEqual(expectedArray6, actualArray6);
            Assert.AreEqual(expectedArray7, actualArray7);
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
            var actualResult1 = converterHelper.CalculateNumberValue(exampleMultipliers1);
            var actualResult2 = converterHelper.CalculateNumberValue(exampleMultipliers2);

            // Assert
            Assert.AreEqual(expectedResult1, actualResult1);
            Assert.AreEqual(expectedResult2, actualResult2);
        }

        [Test]
        public void CalculateMultipliersOfSplitStringTests()
        {
            // Arrange
            var splitString1 = new string[] { "three", "two hundred and sixty seven",
                "nine hundred and ninety nine", "nine hundred and ninety nine", "minus" };
            var expectedResult1 = new int[] { 3, 267, 999, 999, -1 };

            var splitString2 = new string[] { null, null, null, "seven", "negative" };
            var expectedResult2 = new int[] { 0, 0, 0, 7, -1 };

            var splitString3 = new string[] { null, null, null, "four", null };
            var expectedResult3 = new int[] { 0, 0, 0, 4, 1 };

            // Act
            var actualResult1 = converterHelper.CalculateMultipliersOfSplitString(splitString1);
            var actualResult2 = converterHelper.CalculateMultipliersOfSplitString(splitString2);
            var actualResult3 = converterHelper.CalculateMultipliersOfSplitString(splitString3);

            // Assert
            Assert.AreEqual(expectedResult1, actualResult1);
            Assert.AreEqual(expectedResult2, actualResult2);
            Assert.AreEqual(expectedResult3, actualResult3);
        }

        [Test]
        public void ConvertTest()
        {
            // Here we use mocking as all the other methods needed are tested above

            // Arrange
            var number1Str = "Minus two billion, eight hundred and seventy three million, " +
                "three hundred and twenty thousand, nine hundred and ninety nine";
            var number1StrSplit = new string[]
                { "minus two", "eight hundred seventy three", "three hundred twenty", "nine hundred ninety nine" };
            var number1Multipliers = new int[] { 2, 873, 320, 999, -1 };
            var number1Val = -2873320999D;

            var number2Str = "999";

            mockConverterHelper.Setup(m => m.SplitNumberString(number1Str)).Returns(number1StrSplit);
            mockConverterHelper.Setup(m => m.CalculateMultipliersOfSplitString(number1StrSplit)).Returns(number1Multipliers);
            mockConverterHelper.Setup(m => m.CalculateNumberValue(number1Multipliers)).Returns(number1Val);

            var number1 = new Number(number1Str, mockConverterHelper.Object);
            var number2 = new Number(number2Str, mockConverterHelper.Object);

            // Act
            var number1Converted = number1.ToNumber();
            var number2Converted = number2.ToNumber();

            // Assert
            Assert.AreEqual(number1Val, number1Converted);
            mockConverterHelper.Verify(m => m.SplitNumberString(number1Str.ToLower()), Times.Exactly(1));
            mockConverterHelper.Verify(m => m.CalculateMultipliersOfSplitString(number1StrSplit), Times.Exactly(1));
            mockConverterHelper.Verify(m => m.CalculateNumberValue(number1Multipliers), Times.Exactly(1));
        }
    }
}
