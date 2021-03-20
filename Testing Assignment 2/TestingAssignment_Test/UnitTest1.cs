using TestingAssignment;
using System;
using Xunit;

namespace TestingAssignment_Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test_WordCount()
        {
            //Arrange
            var input = "Ayushi Tikoo";
            var expectedValue = 2;
            // Act
            var result = AssignmentBLL.WordCount(input);
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
            var result = AssignmentBLL.NumberValidation(input);
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
            var result = AssignmentBLL.AddLowerCase(input);
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
            var result = AssignmentBLL.AddUpperCase(input);
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
            var result = AssignmentBLL.CheckLowerCase(input);
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
            var result = AssignmentBLL.CheckUpperCase(input);
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
            var result = AssignmentBLL.FirstUpperLetter(input);
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
            var result = AssignmentBLL.LastCharacterRemove(input);
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
            var result = AssignmentBLL.StringToInt(input);
            //Assert
            Assert.Equal(expectedValue, result);
        }
    }
}
