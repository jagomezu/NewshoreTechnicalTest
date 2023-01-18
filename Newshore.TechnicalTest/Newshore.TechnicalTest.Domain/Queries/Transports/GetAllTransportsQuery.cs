using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Transports
{
    public class GetAllTransportsQuery : IRequest<List<TransportResponse>?>
    {
    }

    public class GetAllTransportsQueryHandler : IRequestHandler<GetAllTransportsQuery, List<TransportResponse>?>
    {
        #region Properties
        private readonly ITransportQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetAllTransportsQueryHandler(ITransportQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<List<TransportResponse>?> Handle(GetAllTransportsQuery request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Get all transports] -- Start");
            List<TransportResponse>? result = null;

            try
            {
                List<Transport>? transportList = await _repository.GetAll();

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

                    Log.Information("[DOMAIN Get all transports] -- Success -- Transport founds");
                }
                else
                {
                    Log.Warning($"[DOMAIN Get all transports] -- Success -- Transports not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get all transport] -- Error", ex);

                throw;
            }

            return result;
        }
        #endregion
    }
}
