using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.TechnicalTest.Domain.Queries.Flights
{
    public class GetFlightListByTransportQuery : IRequest<List<FlightResponse>?>
    {
        public int TransportId { get; set; }
    }

    public class GetFlightListByTransportQueryHandler : IRequestHandler<GetFlightListByTransportQuery, List<FlightResponse>?>
    {
        #region Properties
        private readonly IFlightQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetFlightListByTransportQueryHandler(IFlightQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<List<FlightResponse>?> Handle(GetFlightListByTransportQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"[DOMAIN Get flights by transport] -- Start --> transport id: {request.TransportId}");
            List<FlightResponse>? result = null;

            try
            {
                List<Flight>? flightList = await _repository.GetListByTransportId(request.TransportId);

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

                    Log.Information($"[DOMAIN Get flights by transport] -- Success --> transport id: {request.TransportId} -- Flight founds");
                }
                else
                {
                    Log.Warning($"[DOMAIN Get flights by transport] -- Success --> transport id: {request.TransportId} -- Flights not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get flights by transport] -- Error --> transport id: {TransportId}", ex, request.TransportId);

                throw;
            }

            return result;
        }
        #endregion
    }
}
