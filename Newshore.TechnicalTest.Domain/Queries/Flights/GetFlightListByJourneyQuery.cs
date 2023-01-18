using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Flights
{
    public class GetFlightListByJourneyQuery : IRequest<List<FlightResponse>?>
    {
        public int JourneyId { get; set; }
    }

    public class GetFlightListByJourneyQueryHandler : IRequestHandler<GetFlightListByJourneyQuery, List<FlightResponse>?>
    {
        #region Properties
        private readonly IFlightQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetFlightListByJourneyQueryHandler(IFlightQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<List<FlightResponse>?> Handle(GetFlightListByJourneyQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"[DOMAIN Get flights by journey] -- Start --> journey id: {request.JourneyId}");
            List<FlightResponse>? result = null;

            try
            {
                List<Flight>? flightList = await _repository.GetListByJourney(request.JourneyId);

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

                    Log.Information($"[DOMAIN Get flights by journey] -- Success --> journey id: {request.JourneyId} -- Flight founds");
                }
                else
                {
                    Log.Warning($"[DOMAIN Get flights by journey] -- Success --> journey id: {request.JourneyId} -- Flights not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get flights by journey] -- Error --> journey id: {JourneyId}", ex, request.JourneyId);

                throw;
            }

            return result;
        }
        #endregion
    }
}
