using System;
using WongaServiceA;
using WongaServiceB;
using Xunit;

namespace WongaTest
{
    public class WongaServiceBTest
    {
        #region IsValidMessage Tests

        /// <summary>
        /// Test with a valid string
        /// </summary>
        [Fact]
        public void ValidMessage()
        {
            // Arrange
            string testValue = "Hello my name is, Luke";

            // Act
            bool result = WongaServiceB.Program.IsValidMessage(testValue);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Test with a random string
        /// </summary>
        [Fact]
        public void InvalidMessage()
        {
            // Arrange
            string testValue = "This string is invalid";
            // Act
            bool result = WongaServiceB.Program.IsValidMessage(testValue);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Test with a string where the name is ommitted
        /// </summary>
        [Fact]
        public void NoNameMessage()
        {
            // Arrange
            string testValue = "Hello my name is,";

            // Act
            bool result = WongaServiceB.Program.IsValidMessage(testValue);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Test with message that has invalid name
        /// </summary>
        [Fact]
        public void InvalidNameMessage()
        {
            // Arrange
            string testValue = "Hello my name is, Luke5";

            // Act
            bool result = WongaServiceB.Program.IsValidMessage(testValue);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region Extract Name test

        /// <summary>
        /// Extracts name from valid string
        /// </summary>
        [Fact]
        public void ExtractNameSuccessfully()
        {
            // Arrange
            string testValue = "Hello my name is, Luke";

            // Act
            string name = WongaServiceB.Program.ExtractName(testValue);

            // Assert
            Assert.Equal("Luke", name);
        }

        /// <summary>
        /// Extracts name from invalid string, throws exception
        /// </summary>
        [Fact]
        public void ExtractNameWithError()
        {
            // Arrange
            string testValue = "My string";

            // Act and Assert
            Exception ex = Assert.Throws<ArgumentException>(() => WongaServiceB.Program.ExtractName(testValue));

            // Assert
            Assert.Equal(ex.Message, testValue);

        }
        #endregion
    }
}
