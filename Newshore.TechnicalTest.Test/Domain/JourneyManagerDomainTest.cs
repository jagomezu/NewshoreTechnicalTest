using Moq;
using Newshore.TechnicalTest.Domain.Interfaces;
using Newshore.TechnicalTest.Transverse.Dto;

namespace Newshore.TechnicalTest.Test.Domain
{
    public class JourneyManagerDomainTest
    {
        #region Properties
        private Mock<IJourneyManagerDomain>? _mockDomain;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";
        #endregion region

        #region GetJourneysByOriginAndDestination() Tests
        [Fact]
        public void GetJourneysByOriginAndDestination_ExistsJourneys()
        {
            _mockDomain = new();
            string origin = "origin";
            string destination = "destination";

            List<JourneyDto>? journeys = new() { new JourneyDto() { Id = new Random().Next() } };

            _mockDomain.Setup(rep => rep.GetJourneysByOriginAndDestination(origin, destination)).Returns(journeys);

            List<JourneyDto>? testResult = _mockDomain.Object.GetJourneysByOriginAndDestination(origin, destination);

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public void GetJourneysByOriginAndDestination_NotExistsJourneys()
        {
            _mockDomain = new();
            string origin = "origin";
            string destination = "destination";

            List<JourneyDto>? journeys = null;

            _mockDomain.Setup(rep => rep.GetJourneysByOriginAndDestination(origin, destination)).Returns(journeys);

            List<JourneyDto>? testResult = _mockDomain.Object.GetJourneysByOriginAndDestination(origin, destination);

            Assert.Null(testResult);
        }

        [Fact]
        public void GetJourneysByOriginAndDestination_GenerateException()
        {
            try
            {
                _mockDomain = new();
                string origin = "origin";
                string destination = "destination";

                _mockDomain.Setup(rep => rep.GetJourneysByOriginAndDestination(origin, destination)).Throws(new Exception(EXCEPTION_MESSAGE));

                List<JourneyDto>? testResult = _mockDomain.Object.GetJourneysByOriginAndDestination(origin, destination);
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        #endregion


    }
}
