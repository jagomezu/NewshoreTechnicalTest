using MediatR;
using Moq;
using Newshore.TechnicalTest.Domain.Commands.Transports;
using Newshore.TechnicalTest.Domain.ResponseModels;

namespace Newshore.TechnicalTest.Domain.Commands.JourneyTransports
{
    public class TransportCommandsDomainTest
    {
        #region Properties
        private Mock<IMediator>? _mediator;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";
        #endregion

        #region Create() Tests
        [Fact]
        public void Create_Success()
        {
            CreateTransportCommand command = new();

            _mediator = new();
            _mediator
                .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TransportResponse());

            TransportResponse? testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void Create_Error()
        {
            try
            {
                CreateTransportCommand command = new();

                _mediator = new();
                _mediator
                    .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                    .Throws(new Exception(EXCEPTION_MESSAGE));

                TransportResponse? testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        #endregion

        #region Delete() Tests
        [Fact]
        public void Delete_Success()
        {
            DeleteTransportCommand command = new();

            _mediator = new();
            _mediator
                 .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(Unit.Value);

            Unit testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;

            Assert.IsType<Unit>(testResult);
        }

        [Fact]
        public void Delete_Error()
        {
            try
            {
                DeleteTransportCommand command = new();

                _mediator = new();
                _mediator.Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                    .Throws(new Exception(EXCEPTION_MESSAGE));

                _mediator.Object.Send(command, It.IsAny<CancellationToken>());
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        #endregion

        #region Update() Tests
        [Fact]
        public void Update_Success()
        {
            UpdateTransportCommand command = new();

            _mediator = new();
            _mediator
                 .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(Unit.Value);

            Unit testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;

            Assert.IsType<Unit>(testResult);
        }

        [Fact]
        public void Update_Error()
        {
            try
            {
                UpdateTransportCommand command = new();

                _mediator = new();
                _mediator.Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                    .Throws(new Exception(EXCEPTION_MESSAGE));

                _mediator.Object.Send(command, It.IsAny<CancellationToken>());
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        #endregion
    }
}
