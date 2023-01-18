using Newshore.TechnicalTest.Transverse.Entities;

namespace Newshore.TechnicalTest.Infrastructure.Interfaces
{
    public interface IFlightQueriesRepository
    {
        public Task<Flight?> GetById(int flightId);

        public Task<List<Flight>?> GetAll();

        public Task<List<Flight>?> GetListByTransportId(int transportId);

        public Task<List<Flight>?> GetListByPlaces(string? origin, string? destination);

        public Task<List<Flight>?> GetListByJourney(int journeyId);

        public Task<Flight?> GetExistsFlight(Flight flightInfo);
    }
}
