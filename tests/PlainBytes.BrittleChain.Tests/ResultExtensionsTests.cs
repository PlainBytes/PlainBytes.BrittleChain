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
    }
}