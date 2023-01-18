using Moq;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;

namespace Newshore.TechnicalTest.Test.Infrastructure
{
    public class FlightQueriesRepositoryTest
    {
        private Mock<IFlightQueriesRepository>? _mockRepository;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";

        [Fact]
        public void GetAllFlights_ExistsFlights()
        {
            _mockRepository = new();

            List<Flight>? flights = new() { new Flight { Id = new Random().Next() } };

            _mockRepository.Setup(rep => rep.GetAll()).Returns(Task.FromResult(flights));

            List<Flight>? testResult = _mockRepository.Object.GetAll().Result;

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public void GetAllFlights_NotExistsFlights()
        {
            _mockRepository = new();

            List<Flight>? flights = null;

            _mockRepository.Setup(rep => rep.GetAll()).Returns(Task.FromResult(flights));

            List<Flight>? testResult = _mockRepository.Object.GetAll().Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetAllFlights_GenerateException()
        {
            try
            {
                _mockRepository = new();

                _mockRepository.Setup(rep => rep.GetAll()).Throws(new Exception(EXCEPTION_MESSAGE));

                List<Flight>? testResult = _mockRepository.Object.GetAll().Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }

        [Fact]
        public void GetFlightById_ExistsAccount()
        {
            int id = new Random().Next();
            _mockRepository = new();
            Flight validFlight = new() { Id = id };

            _mockRepository.Setup(rep => rep.GetById(id)).Returns(Task.FromResult(validFlight));

            Flight? testResult = _mockRepository.Object.GetById(id).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void GetFlightById_NotExistAccount()
        {
            int id = new Random().Next();
            _mockRepository = new();
            Flight? validFlight = null;

            _mockRepository.Setup(rep => rep.GetById(id)).Returns(Task.FromResult(validFlight));

            Flight? testResult = _mockRepository.Object.GetById(id).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetFlightById_GenerateException()
        {
            try
            {
                int id = new Random().Next();
                _mockRepository = new();
                Flight validFlight = new();
                _mockRepository.Setup(rep => rep.GetById(id)).Throws(new Exception(EXCEPTION_MESSAGE));

                Flight? testResult = _mockRepository.Object.GetById(id).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
    }
}
