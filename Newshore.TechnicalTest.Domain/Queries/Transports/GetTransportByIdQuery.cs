using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Transports
{
    public class GetTransportByIdQuery : IRequest<TransportResponse?>
    {
        public int Id { get; set; }
    }

    public class GetTransportByIdQueryHandler : IRequestHandler<GetTransportByIdQuery, TransportResponse?>
    {
        #region Properties
        private readonly ITransportQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetTransportByIdQueryHandler(ITransportQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<TransportResponse?> Handle(GetTransportByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Get transport by id -- Start --> Id: {@Id}]", request.Id);
            TransportResponse? result = null;

            try
            {
                Transport? transportInfo = await _repository.GetById(request.Id);

                if (transportInfo != null)
                {
                    result = new ()
                    {
                        FlightCarrier = transportInfo.FlightCarrier,
                        FlightNumber = transportInfo.FlightNumber,
                        Id = transportInfo.Id
                    };

                    Log.Information("[DOMAIN Get transport by id -- Success --> Id: {@Id} -- Transport found: {@TransportInfo}", request.Id, result);
                }
                else
                {
                    Log.Warning($"[DOMAIN Get transport by id -- Success --> Id: {request.Id} -- Transport not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get transport by id -- Error --> Id: {@Id}", ex, request.Id);

                throw;
            }

            return result;
        }
        #endregion
    }
}
