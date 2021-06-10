using System;
using PlainBytes.BrittleChain.Synchronous;
using Xunit;

namespace PlainBytes.BrittleChain.Tests
{
    public class MaybeTests
    {
        [Fact]
        public void ToString_GivenException_ThenReturnExceptionMessage()
        {
            // arrange
            const string expected = "Expected message";
            var sut = MaybeExtensions.FromException<string>(new ArgumentException(expected));

            // act
            var actual = sut.ToString();
            
            // assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(5)]
        [InlineData("text")]
        [InlineData(1.234)]
        public void ToString_GivenValue_ThenReturnValueAsString(object input)
        {
            // arrange
            var expected = input.ToString();
            var sut = MaybeExtensions.FromValue(input);

            // act
            var actual = sut.ToString();
            
            // assert
            Assert.Equal(expected, actual);
        }
    }
}