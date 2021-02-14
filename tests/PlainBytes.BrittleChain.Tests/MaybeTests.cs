using System;
using PlainBytes.BrittleChain.Extensions;
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
            var sut = Maybe.FromException<string>(new ArgumentException(expected));

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
            var sut = Maybe.FromValue(input);

            // act
            var actual = sut.ToString();
            
            // assert
            Assert.Equal(expected, actual);
        }
    }
}