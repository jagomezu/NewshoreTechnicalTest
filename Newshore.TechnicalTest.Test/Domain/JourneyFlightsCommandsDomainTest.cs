using MediatR;
using Moq;
using Newshore.TechnicalTest.Domain.Commands.JourneyFlights;

namespace Newshore.TechnicalTest.Test.Domain
{
    public class JourneyFlightsCommandsDomainTest
    {
        #region Properties
        private Mock<IMediator>? _mediator;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";
        #endregion

        #region Create() Tests
        [Fact]
        public void Create_Success()
        {
            CreateJourneyFlightCommand command = new();

            _mediator = new();
            _mediator
                .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            Unit testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;

            Assert.IsType<Unit>(testResult);
        }

        [Fact]
        public void Create_Error()
        {
            try
            {
                CreateJourneyFlightCommand command = new();

                _mediator = new();
                _mediator
                    .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                    .Throws(new Exception(EXCEPTION_MESSAGE));
                
                Unit testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;
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
            DeleteJourneyFlightCommand command = new();

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
                DeleteJourneyFlightCommand command = new();

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
            UpdateJourneyFlightCommand command = new();

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
                UpdateJourneyFlightCommand command = new();

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
