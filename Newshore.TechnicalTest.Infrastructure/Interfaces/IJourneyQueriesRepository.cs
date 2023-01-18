using Newshore.TechnicalTest.Transverse.Entities;

namespace Newshore.TechnicalTest.Infrastructure.Interfaces
{
    public interface IJourneyQueriesRepository
    {
        public Task<Journey?> GetById(int journeyId);

        public Task<List<Journey>?> GetAll();

        public Task<List<Journey>?> GetListByFlightId(int flightId);

        public Task<List<Journey>?> GetListByPlaces(string? origin, string? destination);
    }
}
