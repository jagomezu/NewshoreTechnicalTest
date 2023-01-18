using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Transports
{
    public class GetTransportListByTransportQuery : IRequest<List<TransportResponse>?>
    {
        public int FlightId { get; set; }
    }

    public class GetTransportListByTransportQueryHandler : IRequestHandler<GetTransportListByTransportQuery, List<TransportResponse>?>
    {
        #region Properties
        private readonly ITransportQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetTransportListByTransportQueryHandler(ITransportQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<List<TransportResponse>?> Handle(GetTransportListByTransportQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"[DOMAIN Get transports by flight] -- Start --> flight id: {request.FlightId}");
            List<TransportResponse>? result = null;

            try
            {
                List<Transport>? transportList = await _repository.GetListByFlightId(request.FlightId);

                if (transportList != null)
                {
                    result = new();

                    transportList.ForEach(transportInfo =>
                    {
                        result.Add(new TransportResponse()
                        {
                            FlightCarrier = transportInfo.FlightCarrier,
                            FlightNumber = transportInfo.FlightNumber,
                            Id = transportInfo.Id
                        });
                    });

                    Log.Information($"[DOMAIN Get transports by flight] -- Success --> flight id: {request.FlightId} -- Transports founds");
                }
                else
                {
                    Log.Warning($"[DOMAIN Get transports by flight] -- Success --> flight id: {request.FlightId} -- Transports not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get transports by flight] -- Error --> flight id: {Flight}", ex, request.FlightId);

                throw;
            }

            return result;
        }
        #endregion
    }
}
