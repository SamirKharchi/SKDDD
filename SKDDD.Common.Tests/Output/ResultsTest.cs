
using System;
using SKDDD.Common.Production.Output;
using Xunit;

namespace SKDDD.Common.Tests.Output
{
    public class ResultsTest
    {
        private static void GenericTest(Result<int> testObject, ResultType expectedResultType, string expectedErrorText)
        {
            Assert.True((bool) (testObject.ResultType == expectedResultType));
            Assert.NotEmpty(testObject.Errors);
            Assert.Same(expectedErrorText, testObject.Errors[0]);
            Assert.True(testObject.Data == default);
        }

        [Fact]
        private void TestInvalidResult()
        {
            var result = new InvalidResult<int>("Invalid integer error");
            ResultsTest.GenericTest(result, ResultType.Invalid, "Invalid integer error");
        }

        [Fact]
        private void TestExceptionResult()
        {
            var result = new ExceptionResult<int>(new ArgumentException("Test"));
            ResultsTest.GenericTest(result, ResultType.Exception, "Test");
        }

        [Fact]
        private void TestNotFoundResult()
        {
            var result = new NotFoundResult<int>("Not found");
            ResultsTest.GenericTest(result, ResultType.NotFound, "Not found");
        }

        [Fact]
        private void TestUnauthorizedResult()
        {
            var result = new UnauthorizedResult<int>("Unauthorized access");
            ResultsTest.GenericTest(result, ResultType.Unauthorized, "Unauthorized access");
        }

        [Fact]
        private void TestUnexpectedResult()
        {
            var result = new UnexpectedResult<int>("Unexpected result");
            ResultsTest.GenericTest(result, ResultType.Unexpected, "Unexpected result");
        }

        [Fact]
        private void TestSuccessResult()
        {
            var result = new SuccessResult<int>(100);
            Assert.True((bool) (result.ResultType == ResultType.Ok));
            Assert.Empty(result.Errors);
            Assert.Equal(100, result.Data);
        }
    }
}