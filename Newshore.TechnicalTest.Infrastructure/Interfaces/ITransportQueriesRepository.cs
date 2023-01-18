using Newshore.TechnicalTest.Transverse.Entities;

namespace Newshore.TechnicalTest.Infrastructure.Interfaces
{
    public interface ITransportQueriesRepository
    {
        public Task<Transport?> GetById(int journeyId);

        public Task<List<Transport>?> GetAll();

        public Task<List<Transport>?> GetListByFlightId(int flightId);

        public Task<Transport?> GetExistsTransport(Transport transportInfo);
    }
}
