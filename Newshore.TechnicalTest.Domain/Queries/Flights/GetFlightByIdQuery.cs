using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Flights
{
    public class GetFlightByIdQuery : IRequest<FlightResponse?>
    {
        public int Id { get; set; }
    }

    public class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdQuery, FlightResponse?>
    {
        #region Properties
        private readonly IFlightQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetFlightByIdQueryHandler(IFlightQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<FlightResponse?> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Get flight by id -- Start --> Id: {@Id}]", request.Id);
            FlightResponse? result = null;

            try
            {
                Flight? flightInfo = await _repository.GetById(request.Id);

                if (flightInfo != null)
                {
                    result = new()
                    {
                        Destination= flightInfo.Destination,
                        Id= flightInfo.Id,
                        Origin= flightInfo.Origin,
                        Price = flightInfo.Price,
                        TransportId = flightInfo.TransportId
                    };

                    Log.Information("[DOMAIN Get flight by id -- Success --> Id: {@Id} -- Flight found: {@FlightInfo}", request.Id, result);
                }
                else
                {
                    Log.Warning($"[DOMAIN Get flight by id -- Success --> Id: {request.Id} -- Flight not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get flight by id -- Error --> Id: {@Id}", ex, request.Id);

                throw;
            }

            return result;
        }
        #endregion
    }
}
