using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RJAR.Exceptions.Interfaces;
using RJAR.Exceptions.Tests.Helpers;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Assert = NUnit.Framework.Assert;

namespace RJAR.Exceptions.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ExceptionTests
    {
        private ILogger<FunctionalException> _functionalExceptionLogger;
        private ILogger<TechnicalException> _technicalExceptionLogger;
        private IErrorHandlerFactory _technicalErrorHandlerFactory;
        private IErrorHandlerFactory _functionalErrorHandlerFactory;

        [SetUp]
        public void BaseSetUp()
        {
            _functionalExceptionLogger = new Mock<ILogger<FunctionalException>>().Object;
            _technicalExceptionLogger = new Mock<ILogger<TechnicalException>>().Object;

            var mockTechnicalErrorHandlerFactory = new Mock<IErrorHandlerFactory>();
            mockTechnicalErrorHandlerFactory.Setup(x => x.HandleExceptionResponse(It.IsAny<Exception>())).Returns(ExceptionResponseMessageHelper.GetTechnicalExceptionResponseMessage);

            var mockFunctionalErrorHandlerFactory = new Mock<IErrorHandlerFactory>();
            mockFunctionalErrorHandlerFactory.Setup(x => x.HandleExceptionResponse(It.IsAny<Exception>())).Returns(ExceptionResponseMessageHelper.GetFunctionalExceptionResponseMessage);


            _technicalErrorHandlerFactory = mockTechnicalErrorHandlerFactory.Object;
            _functionalErrorHandlerFactory = mockFunctionalErrorHandlerFactory.Object;
        }

        [TearDown]
        public void BaseTearDown()
        {
            _functionalExceptionLogger = null;
            _technicalExceptionLogger = null;
            _technicalErrorHandlerFactory = null;
            _functionalErrorHandlerFactory = null;
        }

        [Test]
        public void TestFunctionalException()
        {
            var functionalException = new FunctionalException();
            Assert.IsInstanceOf<FunctionalException>(functionalException);
        }

        [Test]
        public void TestFunctionalExceptionWithLogNotConfigured()
        {
            var functionalException = new FunctionalException();

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(functionalException.LogError);
                Assert.IsInstanceOf<FunctionalException>(functionalException);
            });
        }

        [Test]
        public void TestFunctionalExceptionWithLog()
        {
            var functionalException = new FunctionalException();
            functionalException.SetExceptionLogger(_functionalExceptionLogger);

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(functionalException.LogError);
                Assert.IsInstanceOf<FunctionalException>(functionalException);
            });
        }

        [Test]
        public void TestFunctionalExceptionWithMessage()
        {
            var functionalException = new FunctionalException(MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE, functionalException.Message);
                Assert.IsInstanceOf<FunctionalException>(functionalException);
            });
        }

        [Test]
        public void TestFunctionalExceptionWithValidationFields()
        {
            var functionalException = new FunctionalException(MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE, MessageHelper.ValidateFieldMessages);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE, functionalException.Message);
                Assert.IsInstanceOf<FunctionalException>(functionalException);
            });
        }

        [Test]
        public void TestTechnicalException()
        {
            var technicalException = new TechnicalException();
            Assert.IsInstanceOf<TechnicalException>(technicalException);
        }

        [Test]
        public void TestTechnicalExceptionWithLogNotConfigured()
        {
            var technicalException = new TechnicalException();

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(technicalException.LogError);
                Assert.IsInstanceOf<TechnicalException>(technicalException);
            });
        }

        [Test]
        public void TestTechnicalExceptionWithLog()
        {
            var technicalException = new TechnicalException();
            technicalException.SetExceptionLogger(_technicalExceptionLogger);

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(technicalException.LogError);
                Assert.IsInstanceOf<TechnicalException>(technicalException);
            });
        }

        [Test]
        public void TestTechnicalExceptionWithDefaultError()
        {
            var technicalException = new TechnicalException(MessageHelper.EXCEPTION_DEFAULT_MESSAGE);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(MessageHelper.EXCEPTION_DEFAULT_MESSAGE, technicalException.Message);
                Assert.IsInstanceOf<TechnicalException>(technicalException);
            });
        }

        [Test]
        public void TestTechnicalExceptionWithCustomError()
        {
            var technicalException = new TechnicalException(MessageHelper.TECHNICAL_EXCEPTION_MESSAGE);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(MessageHelper.TECHNICAL_EXCEPTION_MESSAGE, technicalException.Message);
                Assert.IsInstanceOf<TechnicalException>(technicalException);
            });
        }

        [Test]
        public void TestErrorHandlerFactoryWithTechnicalException()
        {
            var technicalException = new TechnicalException(MessageHelper.TECHNICAL_EXCEPTION_MESSAGE);
            var messageResponse = _technicalErrorHandlerFactory.HandleExceptionResponse(technicalException);

            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<TechnicalException>(technicalException);
                Assert.AreEqual(MessageHelper.TECHNICAL_EXCEPTION_MESSAGE, technicalException.Message);
                Assert.IsInstanceOf<IBaseExceptionResponseMessage>(messageResponse);
                Assert.AreEqual(messageResponse.StatusCode, (Int32) HttpStatusCode.InternalServerError);
                Assert.AreEqual(MessageHelper.EXCEPTION_DEFAULT_MESSAGE, messageResponse.ErrorMessage);
            });
        }

        [Test]
        public void TestErrorHandlerFactoryWithTechnicalExceptionAndLogger()
        {
            var technicalException = new TechnicalException(MessageHelper.TECHNICAL_EXCEPTION_MESSAGE);
            technicalException.SetExceptionLogger(_technicalExceptionLogger);

            var messageResponse = _technicalErrorHandlerFactory.HandleExceptionResponse(technicalException);

            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<TechnicalException>(technicalException);
                Assert.AreEqual(MessageHelper.TECHNICAL_EXCEPTION_MESSAGE, technicalException.Message);
                Assert.IsInstanceOf<IBaseExceptionResponseMessage>(messageResponse);
                Assert.AreEqual(messageResponse.StatusCode, (Int32) HttpStatusCode.InternalServerError);
                Assert.AreEqual(MessageHelper.EXCEPTION_DEFAULT_MESSAGE, messageResponse.ErrorMessage);
                Assert.DoesNotThrow(technicalException.LogError);
            });
        }

        [Test]
        public void TestErrorHandlerFactoryWithFunctionalException()
        {
            var functionalException = new FunctionalException(MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE);
            var messageResponse = _functionalErrorHandlerFactory.HandleExceptionResponse(functionalException);

            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<FunctionalException>(functionalException);
                Assert.AreEqual(MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE, functionalException.Message);
                Assert.IsInstanceOf<IFunctionalExceptionResponseMessage>(messageResponse);
                Assert.AreEqual(messageResponse.StatusCode, (Int32) HttpStatusCode.BadRequest);
                Assert.AreEqual(MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE, messageResponse.ErrorMessage);
            });
        }

        [Test]
        public void TestErrorHandlerFactoryWithFunctionalExceptionAndLogger()
        {
            var functionalException = new FunctionalException(MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE);
            functionalException.SetExceptionLogger(_functionalExceptionLogger);

            var messageResponse = _functionalErrorHandlerFactory.HandleExceptionResponse(functionalException);

            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<FunctionalException>(functionalException);
                Assert.AreEqual(MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE, functionalException.Message);
                Assert.IsInstanceOf<IFunctionalExceptionResponseMessage>(messageResponse);
                Assert.AreEqual(messageResponse.StatusCode, (Int32) HttpStatusCode.BadRequest);
                Assert.AreEqual(MessageHelper.FUNCTIONAL_EXCEPTION_MESSAGE, messageResponse.ErrorMessage); 
                Assert.DoesNotThrow(functionalException.LogError);
            });
        }
    }
}
