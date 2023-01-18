using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Flights
{
    public class GetFlightListByPlacesQuery : IRequest<List<FlightResponse>?>
    {
        public string Origin { get; set; } = string.Empty;

        public string Destination { get; set; } = string.Empty;
    }

    public class GetFlightListByPlacesQueryHandler : IRequestHandler<GetFlightListByPlacesQuery, List<FlightResponse>?>
    {
        #region Properties
        private readonly IFlightQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetFlightListByPlacesQueryHandler(IFlightQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<List<FlightResponse>?> Handle(GetFlightListByPlacesQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"[DOMAIN Get flights by places] -- Start --> Origin: {request.Origin}, Destination: {request.Destination}");
            List<FlightResponse>? result = null;

            try
            {
                List<Flight>? flightList = await _repository.GetListByPlaces(request.Origin, request.Destination);

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

                    Log.Information($"[DOMAIN Get flights by places] -- Success --> Origin: {request.Origin}, Destination: {request.Destination} -- Flight founds");
                }
                else
                {
                    Log.Warning($"[DOMAIN Get flights by places] -- Success --> Origin: {request.Origin}, Destination: {request.Destination} -- Flights not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get flights by places] -- Error --> Origin: {Origin}, Destination: {Destination}", ex, request.Origin, request.Destination);

                throw;
            }

            return result;
        }
        #endregion
    }

}
