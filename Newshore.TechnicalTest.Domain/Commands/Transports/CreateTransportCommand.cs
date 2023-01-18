using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.TechnicalTest.Domain.Commands.Transports
{
    public class CreateTransportCommand : IRequest<TransportResponse>
    {
        [MaxLength(100)]
        public string FlightCarrier { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FlightNumber { get; set; } = string.Empty;
    }

    public class CreateTransportCommandHandler : IRequestHandler<CreateTransportCommand, TransportResponse>
    {
        #region Properties
        private readonly ITransportCommandsRepository _repository;
        private readonly ITransportQueriesRepository _queriesRepository;
        #endregion

        #region Constructor
        public CreateTransportCommandHandler(ITransportCommandsRepository repository, ITransportQueriesRepository queriesRepository)
        {
            _repository = repository;
            _queriesRepository = queriesRepository;
        }
        #endregion

        #region Public methods
        public async Task<TransportResponse> Handle(CreateTransportCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Create Transport] -- Start --> Transport Info: {@TransportInfo}", request);
            TransportResponse result = null;

            try
            {
                Transport newTransport = new()
                {
                    FlightCarrier = request.FlightCarrier,
                    FlightNumber = request.FlightNumber
                };

                Transport? transportInfo = await _queriesRepository.GetExistsTransport(newTransport);

                if (transportInfo == null)
                {
                    transportInfo = await _repository.Create(newTransport);
                }

                Log.Information("[DOMAIN Create Transport] -- Success --> Transport Info: {@TransportInfo}", request);

                if (transportInfo != null)
                {
                    result = new()
                    {
                        Id = transportInfo.Id,
                        FlightCarrier = transportInfo.FlightCarrier,
                        FlightNumber = transportInfo.FlightNumber
                    };
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Create Transport] -- Error --> Transport Info: {@TransportInfo}", ex, request);
                throw;
            }

            return result;
        }
        #endregion
    }
}
