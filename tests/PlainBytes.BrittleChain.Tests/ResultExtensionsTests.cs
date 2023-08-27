using System;
using FluentAssertions;
using Xunit;

namespace PlainBytes.BrittleChain.Tests
{
    public class ResultExtensionsTests
    {
        [Fact]
        public void ToResult_GivenValue_ThanSucceed()
        {
            // Act
            var result = "test".ToResult();

            // Assert
            result.Exception.Should().BeNull();
            result.Value.Should().Be("test");
            result.Succeeded.Should().BeTrue();
            result.Failed.Should().BeFalse();
        }

        [Fact]
        public void ToResult_GivenException_ThanFail()
        {
            // Act
            var result = new Exception().ToResult();

            // Assert
            result.Exception.Should().NotBeNull();
            result.Value.Should().BeNull();
            result.Succeeded.Should().BeFalse();
            result.Failed.Should().BeTrue();
        }

        [Fact]
        public void ToResultResult_GivenResult_ThanSucceed()
        {
            // Act
            var result = "test".ToResultTask().Result;

            // Assert
            result.Exception.Should().BeNull();
            result.Value.Should().Be("test");
            result.Succeeded.Should().BeTrue();
            result.Failed.Should().BeFalse();
        }

        [Fact]
        public void ToResultResult_GivenException_ThanFail()
        {
            // Act
            var result = new Exception().ToResultTask().Result;

            // Assert
            result.Exception.Should().NotBeNull();
            result.Value.Should().BeNull();
            result.Succeeded.Should().BeFalse();
            result.Failed.Should().BeTrue();
        }

        [Fact]
        public void Map_GivenSuccess_ThanExecuteOnSuccessCallback()
        {
            // Arrange
            var expectedResult = "Test";
            
            var result = expectedResult.ToResult();
            var successCalls = 0;
            var failureCalls = 0;

            // Act
            result.Map(x =>
            {
                x.Should().Be(expectedResult);
                successCalls++;
            },_ => failureCalls++);

            // Assert
            successCalls.Should().Be(1);
            failureCalls.Should().Be(0);
        }

        [Fact]
        public void Map_GivenFailure_ThanExecuteOnFailureCallback()
        {
            // Arrange
            var expectedException = new Exception("test");
            
            var result = Result<string>.CreateFromException(expectedException);
            var successCalls = 0;
            var failureCalls = 0;

            // Act
            result.Map(x =>
            {
                successCalls++;
            }, exception =>
            {
                exception.Should().Be(expectedException);
                failureCalls++;
            });

            // Assert
            successCalls.Should().Be(0);
            failureCalls.Should().Be(1);
        }

        [Fact]
        public void WhenFailedWith_WhenExpectedException_ThanExecuteCallback()
        {
            // Arrange
            var expectedException = new ArgumentException("test");
            
            var result = Result<string>.CreateFromException(expectedException);
            int calls = 0;
            
            // Act
            result.WhenFailedWith<string, ArgumentException>(exception =>
            {
                exception.Should().Be(expectedException);
                calls++;
            });
            
            // Assert
            calls.Should().Be(1);
        }
        
        [Fact]
        public void WhenFailedWith_WhenDifferentException_ThanExecuteCallback()
        {
            // Arrange
            var expectedException = new ArgumentException("test");
            
            var result = Result<string>.CreateFromException(expectedException);
            var calls = 0;
            
            // Act
            result.WhenFailedWith<string, Exception>(exception =>
            {
                exception.Should().Be(expectedException);
            });
            
            // Assert
            calls.Should().Be(0);
        }
    }
}