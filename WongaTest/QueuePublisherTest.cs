using System;
using WongaServiceA;
using WongaServiceB;
using Xunit;

namespace WongaTest
{
    public class QueuePublisherTest
    {
        [Fact]
        public void ValidMessage()
        {
            // Arrange
            // Act
            bool result = WongaServiceB.Program.IsValidMessage("Hello my name is, Luke");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InvalidMessage()
        {
            // Arrange
            // Act
            bool result = WongaServiceB.Program.IsValidMessage("Luke");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void NoNameMessage()
        {
            // Arrange
            // Act
            bool result = WongaServiceB.Program.IsValidMessage("Hello my name is,");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void InvalidNameMessage()
        {
            // Arrange
            // Act
            bool result = WongaServiceB.Program.IsValidMessage("Hello my name is, Luke5");

            // Assert
            Assert.False(result);
        }
    }
}
