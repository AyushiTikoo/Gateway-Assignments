using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingAssignment2;
using System;
using Xunit;
using Assert = Xunit.Assert;

namespace TestingAssignment2_Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test_WordCount()
        {
            // Arrange
            var input = "Ayushi Tikoo";
            var expectedValue = 2;
            // Act
            var result = input.WordCount();
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_NumberValidation()
        {
            // Arrange
            var input = "354";
            var expectedValue = true;
            // Act
            var result = input.NumberValidation();
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_AddLowerCase()
        {
            // Arrange
            var input = "AYUSHI";
            var expectedValue = "ayushi";
            // Act
            var result = input.ConvertLowerCase();
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_AddUpperCase()
        {
            // Arrange
            var input = "ayushi";
            var expectedValue = "AYUSHI";
            // Act
            var result = input.ConvertUpperCase();
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_CheckLowerCase()
        {
            // Arrange
            var input = "how to do";
            var expectedValue = true;
            // Act
            var result = input.CheckLowerCase();
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_CheckUpperCase()
        {
            // Arrange
            var input = "AYUSHI";
            var expectedValue = true;
            // Act
            var result = input.CheckUpperCase();
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_FirstUpperLetter()
        {
            // Arrange
            var input = "ayushi";
            var expectedValue = "Ayushi";
            // Act
            var result = input.FirstUpperLetter();
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_LastCharacterRemove()
        {
            // Arrange
            var input = "ayushi";
            var expectedValue = "ayush";
            // Act
            var result = input.LastCharacterRemove();
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_StringToInt()
        {
            // Arrange
            var input = "354";
            var expectedValue = 354;
            // Act
            var result = input.StringToInt();
            // Assert
            Assert.Equal(expectedValue, result);
        }
    }
}
