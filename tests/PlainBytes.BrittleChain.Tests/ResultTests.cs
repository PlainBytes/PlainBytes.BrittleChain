using System;
using FluentAssertions;
using Xunit;

namespace PlainBytes.BrittleChain.Tests
{
    public class ResultTests
    {
        [Fact]
        public void FromValue_GivenNullValue_ThanFail()
        {
            // Arrange
            
            // Act
            var result = Result<string>.FromValue(null);

            // Assert
            result.Succeeded.Should().BeFalse();
            result.Failed.Should().BeTrue();
        }

        [Fact]
        public void FromValue_GivenValue_ThanSucceed()
        {
            // Arrange
            var expected = "test";
            
            // Act
            var result = Result<string>.FromValue(expected);

            // Assert
            result.Value.Should().Be(expected);
            result.Succeeded.Should().BeTrue();
            result.Failed.Should().BeFalse();
        }

        [Fact]
        public void FromException_GivenException_ThanFail()
        {
            // Act
            var result = Result<string>.FromException( new ArgumentException());

            // Assert
            result.Succeeded.Should().BeFalse();
            result.Failed.Should().BeTrue();
            result.Exception.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void ImplicitConversion_FromResult_ToValue_ThanShouldSucceed()
        {
            // Arrange
            var expected = "test";
            
            // Act
            string result = Result<string>.FromValue(expected);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void ImplicitConversion_FromResult_ToValue_ThanShouldFail()
        {
            // Arrange
            var result = Result<string>.FromException( new ArgumentException());

            void AssertAction()
            {
                string value = result;
            }
            
            // Assert
            Assert.Throws<ArgumentException>(AssertAction);
        }
        
        [Fact]
        public void ImplicitConversion_GivenResult()
        {
            // Arrange
            var expected = "test";

            // Act
            Result<string> result = expected;

            // Assert
            result.Value.Should().Be(expected);
        }

        [Fact]
        public void ImplicitConversion_GivenException()
        {
            // Act
            Result<string> result = new ArgumentException();

            // Assert
            result.Succeeded.Should().BeFalse();
            result.Failed.Should().BeTrue();
            result.Exception.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void ToString_GivenValue_ThanShouldReturnValue()
        {
            // Arrange
            var expected = "test";
            
            // Act
            var result = Result<string>.FromValue(expected);

            // Assert
            result.ToString().Should().Be(expected);
        }

        [Fact]
        public void ToString_GivenException_ThanShouldReturnValue()
        {
            // Act
            var expected = "test";
            var result = Result<string>.FromException(new ArgumentException(expected));

            // Assert
            result.ToString().Should().Be(expected);
        }
    }
}