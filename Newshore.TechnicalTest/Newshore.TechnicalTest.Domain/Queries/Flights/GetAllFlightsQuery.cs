using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Flights
{
    public class GetAllFlightsQuery : IRequest<List<FlightResponse>?>
    {
    }

    public class GetAllFlightsQueryHandler : IRequestHandler<GetAllFlightsQuery, List<FlightResponse>?>
    {
        #region Properties
        private readonly IFlightQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetAllFlightsQueryHandler(IFlightQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<List<FlightResponse>?> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Get all flights] -- Start");
            List<FlightResponse>? result = null;

            try
            {
                List<Flight>? flightList = await _repository.GetAll();

                if (flightList != null)
                {
                    result = new();

                    flightList.ForEach(flightInfo =>
                    {
                        result.Add(new FlightResponse()
                        {
                            Destination = flightInfo.Destination,
                            Id = flightInfo.Id,
                            Origin = flightInfo.Origin,
                            Price = flightInfo.Price,
                            TransportId = flightInfo.TransportId
                        });
                    });

                    Log.Information("[DOMAIN Get all flights] -- Success -- Flight founds");
                }
                else
                {
                    Log.Warning($"[DOMAIN Get all flights] -- Success -- Flights not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get all flight] -- Error", ex);

                throw;
            }

            return result;
        }
        #endregion
    }
}
