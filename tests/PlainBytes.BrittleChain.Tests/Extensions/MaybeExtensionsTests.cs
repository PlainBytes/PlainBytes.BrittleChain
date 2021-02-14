using System;
using PlainBytes.BrittleChain.Extensions;
using Xunit;

namespace PlainBytes.BrittleChain.Tests.Extensions
{
    public class MaybeExtensionsTests
    {
        [Fact]
        public void FromValue_GivenNull_ThenSetException()
        {
            // Arrange

            // Act
            var result = Maybe.FromValue<object>(null); 
            
            // Assert
            Assert.IsType<ArgumentException>(result.Exception);
        }

        [Theory]
        [InlineData(123)]
        [InlineData(1.23)]
        [InlineData("text-string")]
        [InlineData(new []{1,2,3})]
        public void FromValue_GivenValue_ThenSetAsValue(object input)
        {
            // Arrange
            var expectedType = input.GetType();
            
            // Act
            var result = Maybe.FromValue(input);

            // Assert
            Assert.Equal(input, result.Value);
            Assert.IsType(expectedType, result.Value);
        }
        
        [Fact]
        public void AsMaybe_GivenNull_ThenSetException()
        {
            // Arrange

            // Act
            var result = Maybe.AsMaybe<string>(null); 
            
            // Assert
            Assert.IsType<ArgumentException>(result.Exception);
        }

        [Theory]
        [InlineData(123)]
        [InlineData(1.23)]
        [InlineData("text-string")]
        [InlineData(new []{1,2,3})]
        public void AsMaybe_GivenValue_ThenSetAsValue(object input)
        {
            // Arrange
            var expectedType = input.GetType();
            
            // Act
            var result = input.AsMaybe();

            // Assert
            Assert.Equal(input, result.Value);
            Assert.IsType(expectedType, result.Value);
        }

        [Fact]
        public void FromException_GivenException_ThenSetAsException()
        {
            // Arrange
            var expectedMessage = "exception message";
            var expected = new NullReferenceException(expectedMessage);
            
            // Act
            var result = Maybe.FromException<int>(expected);

            // Assert
            Assert.Equal(expected, result.Exception);
            Assert.Equal(expectedMessage, result.Exception.Message);
        }

        [Fact]
        public void Chain_GivenInputHasValue_ThenCallAction()
        {
            // Arrange
            var numberOfCalls = 0;
            void OnValue(string _) => numberOfCalls++;
            var source = Maybe.FromValue(string.Empty);

            // Act
            source.Chain(OnValue);

            // Assert
            Assert.Equal(1, numberOfCalls);
        }

        [Fact]
        public void Chain_GivenInputHasValue_ThenReturnSource()
        {
            // Arrange
            void OnValue(string _) { }
            var expected = Maybe.FromValue(string.Empty);

            // Act
            var result = expected.Chain(OnValue);

            // Assert
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Chain_GivenInputHasException_ThenDoNotCallAction()
        {
            // Arrange
            var numberOfCalls = 0;
            void OnValue(string _) => numberOfCalls++;
            var source = Maybe.FromException<string>(new Exception());

            // Act
            source.Chain(OnValue);

            // Assert
            Assert.Equal(0, numberOfCalls);
        }

        [Fact]
        public void Chain_GivenActionThrowsException_ThenReturnException()
        {
            // Arrange
            var expectedMessage = "expected-message";
            void OnValue(string _) => throw new ArithmeticException(expectedMessage);
            var source = Maybe.FromValue(string.Empty);

            // Act
            var result = source.Chain(OnValue);

            // Assert
            Assert.IsType<ArithmeticException>(result.Exception);
            Assert.Equal(expectedMessage, result.Exception.Message);
        }
        
        [Fact]
        public void Chain_OnError_GivenInputHasValue_ThenCallAction()
        {
            // Arrange
            var numberOfCalls = 0;
            void OnValue(string _) => numberOfCalls++;
            void OnError(Exception e) {}
            var source = Maybe.FromValue(string.Empty);

            // Act
            source.Chain(OnValue,OnError);

            // Assert
            Assert.Equal(1, numberOfCalls);
        }

        [Fact]
        public void Chain_OnError_GivenInputHasValue_ThenReturnSource()
        {
            // Arrange
            void OnValue(string _) { }
            void OnError(Exception e) {}
            var expected = Maybe.FromValue(string.Empty);
            
            // Act
            var result = expected.Chain(OnValue, OnError);

            // Assert
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Chain_OnError_GivenInputHasException_ThenDoNotCallAction()
        {
            // Arrange
            var numberOfCalls = 0;
            void OnValue(string _) => numberOfCalls++;
            void OnError(Exception e) {}
            var source = Maybe.FromException<string>(new Exception());

            // Act
            source.Chain(OnValue, OnError);

            // Assert
            Assert.Equal(0, numberOfCalls);
        }

        [Fact]
        public void Chain_OnError_GivenActionThrowsException_ThenReturnException()
        {
            // Arrange
            var expectedMessage = "expected-message";
            void OnValue(string _) => throw new ArithmeticException(expectedMessage);
            void OnError(Exception e) {}
            var source = Maybe.FromValue(string.Empty);

            // Act
            var result = source.Chain(OnValue, OnError);

            // Assert
            Assert.IsType<ArithmeticException>(result.Exception);
            Assert.Equal(expectedMessage, result.Exception.Message);
        }
        
        [Fact]
        public void Chain_OnError_GivenActionThrowsException_ThenCallOnErrorWithException()
        {
            // Arrange
            var numberOfCalls = 0;
            object onErrorException = null;
            var expectedMessage = "expected-message";
            void OnValue(string _) => throw new ArithmeticException(expectedMessage);
            void OnError(Exception e)
            {
                onErrorException = e;
                numberOfCalls++;
            }
            var source = Maybe.FromValue(string.Empty);

            // Act
            source.Chain(OnValue, OnError);

            // Assert
            Assert.Equal(1, numberOfCalls);
            Assert.IsType<ArithmeticException>(onErrorException);
            Assert.Equal(expectedMessage, (onErrorException as Exception)?.Message);
        }

        [Fact]
        public void Do_GivenInputHasValue_ThenCallAction()
        {
            // Arrange
            var numberOfCalls = 0;
            void OnValue(string _) => numberOfCalls++;
            var source = Maybe.FromValue(string.Empty);

            // Act
            source.Do(OnValue);

            // Assert
            Assert.Equal(1, numberOfCalls);
        }

        [Fact]
        public void Do_GivenInputHasValue_ThenReturnSource()
        {
            // Arrange
            void OnValue(string _) { }
            var expected = Maybe.FromValue(string.Empty);

            // Act
            var result = expected.Do(OnValue);

            // Assert
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Do_GivenInputHasException_ThenDoNotCallAction()
        {
            // Arrange
            var numberOfCalls = 0;
            void OnValue(string _) => numberOfCalls++;
            var source = Maybe.FromException<string>(new Exception());

            // Act
            source.Do(OnValue);

            // Assert
            Assert.Equal(0, numberOfCalls);
        }

        [Fact]
        public void Do_GivenActionThrowsException_ThenReturnSource()
        {
            // Arrange
            var expectedMessage = "expected-message";
            void OnValue(string _) => throw new ArithmeticException(expectedMessage);
            var source = Maybe.FromValue(string.Empty);

            // Act
            var result = source.Do(OnValue);

            // Assert
            Assert.Equal(source, result);
        }
        
        [Fact]
        public void Do_OnError_GivenInputHasValue_ThenCallAction()
        {
            // Arrange
            var numberOfCalls = 0;
            void OnValue(string _) => numberOfCalls++;
            void OnError(Exception e) {}
            var source = Maybe.FromValue(string.Empty);

            // Act
            source.Do(OnValue,OnError);

            // Assert
            Assert.Equal(1, numberOfCalls);
        }

        [Fact]
        public void Do_OnError_GivenInputHasValue_ThenReturnSource()
        {
            // Arrange
            void OnValue(string _) { }
            void OnError(Exception e) {}
            var expected = Maybe.FromValue(string.Empty);
            
            // Act
            var result = expected.Do(OnValue, OnError);

            // Assert
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Do_OnError_GivenInputHasException_ThenDoNotCallAction()
        {
            // Arrange
            var numberOfCalls = 0;
            void OnValue(string _) => numberOfCalls++;
            void OnError(Exception e) {}
            var source = Maybe.FromException<string>(new Exception());

            // Act
            source.Do(OnValue, OnError);

            // Assert
            Assert.Equal(0, numberOfCalls);
        }

        [Fact]
        public void Do_OnError_GivenActionThrowsException_ThenReturnSource()
        {
            // Arrange
            var expectedMessage = "expected-message";
            void OnValue(string _) => throw new ArithmeticException(expectedMessage);
            void OnError(Exception e) {}
            var source = Maybe.FromValue(string.Empty);

            // Act
            var result = source.Do(OnValue, OnError);

            // Assert
            Assert.Equal(source, result);
        }
        
        [Fact]
        public void Do_OnError_GivenActionThrowsException_ThenCallOnErrorWithException()
        {
            // Arrange
            var numberOfCalls = 0;
            object onErrorException = null;
            var expectedMessage = "expected-message";
            void OnValue(string _) => throw new ArithmeticException(expectedMessage);
            void OnError(Exception e)
            {
                onErrorException = e;
                numberOfCalls++;
            }
            var source = Maybe.FromValue(string.Empty);

            // Act
            source.Do(OnValue, OnError);

            // Assert
            Assert.Equal(1, numberOfCalls);
            Assert.IsType<ArithmeticException>(onErrorException);
            Assert.Equal(expectedMessage, (onErrorException as Exception)?.Message);
        }

        [Fact]
        public void OnFail_HasValue_ThenDoNotCallOnErrorWithException()
        {
            // Arrange
            var numberOfCalls = 0;
            void OnError(Exception e) => numberOfCalls++;
            var source = Maybe.FromValue(string.Empty);
            
            // Act
            source.OnFail(OnError);
            
            // Assert
            Assert.Equal(0, numberOfCalls);
        }

        [Fact]
        public void OnFail_Always_ReturnsItsSource()
        {
            // Arrange
            var source = Maybe.FromValue(string.Empty);
            void OnError(Exception e) {}
            
            // Act
            var result = source.OnFail(OnError);

            // Assert
            Assert.Equal(source, result);
        }
        
        [Fact]
        public void OnFail_HasNoValue_ThenCallOnErrorWithException()
        {
            // Arrange
            var numberOfCalls = 0;
            object onErrorException = null;
            var expectedMessage = "expected-message";
            void OnError(Exception e)
            {
                onErrorException = e;
                numberOfCalls++;
            }
            var source = Maybe.FromException<string>(new ArithmeticException(expectedMessage));
            
            // Act
            source.OnFail(OnError);
            
            // Assert
            Assert.Equal(1, numberOfCalls);
            Assert.IsType<ArithmeticException>(onErrorException);
            Assert.Equal(expectedMessage, (onErrorException as Exception)?.Message);
        }
        
        [Fact]
        public void Shape_GivenInputHasValue_ThenCallFunction()
        {
            // Arrange
            var numberOfCalls = 0;

            string OnValue(string input)
            {
                numberOfCalls++;
                return input;
            }
            var source = Maybe.FromValue(string.Empty);

            // Act
            source.Shape(OnValue);

            // Assert
            Assert.Equal(1, numberOfCalls);
        }

        [Fact]
        public void Shape_GivenInputHasValue_ThenReturnResultOfFunction()
        {
            // Arrange
            var expectedValue = 5;

            int OnValue(string _)
            {
                return expectedValue;
            }
            var expected = Maybe.FromValue(string.Empty);

            // Act
            var result = expected.Shape(OnValue);

            // Assert
            Assert.Equal(expectedValue, result.Value);
        }
        
        [Fact]
        public void Shape_GivenInputHasException_ThenDoNotCallFunction()
        {
            // Arrange
            var numberOfCalls = 0;
            string OnValue(string input)
            {
                numberOfCalls++;
                return input;
            }
            var source = Maybe.FromException<string>(new Exception());

            // Act
            source.Shape(OnValue);

            // Assert
            Assert.Equal(0, numberOfCalls);
        }
        
        [Fact]
        public void Shape_GivenInputHasException_ThenPassAlongException()
        {
            // Arrange
            var expectedMessage = "expected-message";
            int OnValue(string _) => throw new ArithmeticException(expectedMessage);
            var source = Maybe.FromValue(string.Empty);
            
            // Act
            var result = source.Shape(OnValue);

            // Assert
            Assert.IsType<ArithmeticException>(result.Exception);
            Assert.Equal(expectedMessage, result.Exception.Message);
        }

        [Fact]
        public void Shape_GivenFunctionThrowsException_ThenReturnException()
        {
            // Arrange
            var expectedMessage = "expected-message";
            int OnValue(string _) => throw new ArithmeticException(expectedMessage);
            var source = Maybe.FromValue(string.Empty);

            // Act
            var result = source.Shape(OnValue);

            // Assert
            Assert.IsType<ArithmeticException>(result.Exception);
            Assert.Equal(expectedMessage, result.Exception.Message);
        }
        
        [Fact]
        public void Shape_OnError_GivenInputHasValue_ThenCallFunction()
        {
            // Arrange
            var numberOfCalls = 0;

            string OnValue(string input)
            {
                numberOfCalls++;
                return input;
            }
            void OnError(Exception e){}
            var source = Maybe.FromValue(string.Empty);

            // Act
            source.Shape(OnValue,OnError);

            // Assert
            Assert.Equal(1, numberOfCalls);
        }

        [Fact]
        public void Shape_OnError_GivenInputHasValue_ThenReturnResultOfFunction()
        {
            // Arrange
            var expectedValue = 5;

            int OnValue(string _)
            {
                return expectedValue;
            }
            void OnError(Exception e){}
            var expected = Maybe.FromValue(string.Empty);

            // Act
            var result = expected.Shape(OnValue,OnError);

            // Assert
            Assert.Equal(expectedValue, result.Value);
        }
        
        [Fact]
        public void Shape_OnError_GivenInputHasException_ThenDoNotCallFunction()
        {
            // Arrange
            var numberOfCalls = 0;
            string OnValue(string input)
            {
                numberOfCalls++;
                return input;
            }
            void OnError(Exception e){}
            var source = Maybe.FromException<string>(new Exception());

            // Act
            source.Shape(OnValue,OnError);

            // Assert
            Assert.Equal(0, numberOfCalls);
        }
        
        [Fact]
        public void Shape_OnError_GivenInputHasException_ThenPassAlongException()
        {
            // Arrange
            var expectedMessage = "expected-message";
            int OnValue(string _) => throw new ArithmeticException(expectedMessage);
            void OnError(Exception e){}
            var source = Maybe.FromValue(string.Empty);
            
            // Act
            var result = source.Shape(OnValue,OnError);

            // Assert
            Assert.IsType<ArithmeticException>(result.Exception);
            Assert.Equal(expectedMessage, result.Exception.Message);
        }

        [Fact]
        public void Shape_OnError_GivenFunctionThrowsException_ThenReturnException()
        {
            // Arrange
            var expectedMessage = "expected-message";
            int OnValue(string _) => throw new ArithmeticException(expectedMessage);
            void OnError(Exception e){}
            var source = Maybe.FromValue(string.Empty);

            // Act
            var result = source.Shape(OnValue,OnError);

            // Assert
            Assert.IsType<ArithmeticException>(result.Exception);
            Assert.Equal(expectedMessage, result.Exception.Message);
        }
        
        [Fact]
        public void Shape_OnError_GivenFunctionThrowsException_ThenCallOnError()
        {
            // Arrange
            var numberOfCalls = 0;
            object onErrorException = null;
            var expectedMessage = "expected-message";
            int OnValue(string _) => throw new ArithmeticException(expectedMessage);
            void OnError(Exception e)
            {
                onErrorException = e;
                numberOfCalls++;
            }
            var source = Maybe.FromValue(string.Empty);

            // Act
            source.Shape(OnValue,OnError);

            // Assert
            Assert.Equal(1, numberOfCalls);
            Assert.IsType<ArithmeticException>(onErrorException);
            Assert.Equal(expectedMessage, (onErrorException as Exception)?.Message);
        }
    }
}