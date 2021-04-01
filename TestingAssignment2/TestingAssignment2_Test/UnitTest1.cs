using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingAssignment2;
using System;
using Xunit;
using Assert = Xunit.Assert;

namespace TestingAssignment2_Test
{
    [TestClass]
    public class UnitTest1
    {
        [Fact]
        public void Test_WordCount()
        {
            //Arrange
            var input = "Ayushi Tikoo";
            var expectedValue = 2;
            // Act
            var result = Extension.WordCount(input);
            //Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_NumberValidation()
        {
            //Arrange
            var input = "354";
            var expectedValue = true;
            // Act
            var result = Extension.NumberValidation(input);
            //Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_AddLowerCase()
        {
            //Arrange
            var input = "AYUSHI";
            var expectedValue = "ayushi";
            // Act
            var result = Extension.ConvertLowerCase(input);
            //Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_AddUpperCase()
        {
            //Arrange
            var input = "ayushi";
            var expectedValue = "AYUSHI";
            // Act
            var result = Extension.ConvertUpperCase(input);
            //Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_CheckLowerCase()
        {
            //Arrange
            var input = "how to do";
            var expectedValue = true;
            // Act
            var result = Extension.CheckLowerCase(input);
            //Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_CheckUpperCase()
        {
            //Arrange
            var input = "AYUSHI";
            var expectedValue = true;
            // Act
            var result = Extension.CheckUpperCase(input);
            //Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_FirstUpperLetter()
        {
            //Arrange
            var input = "ayushi";
            var expectedValue = "Ayushi";
            // Act
            var result = Extension.FirstUpperLetter(input);
            //Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_LastCharacterRemove()
        {
            //Arrange
            var input = "ayushi";
            var expectedValue = "ayush";
            // Act
            var result = Extension.LastCharacterRemove(input);
            //Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Test_StringToInt()
        {
            //Arrange
            var input = "354";
            var expectedValue = 354;
            // Act
            var result = Extension.StringToInt(input);
            //Assert
            Assert.Equal(expectedValue, result);
        }
    }
}
