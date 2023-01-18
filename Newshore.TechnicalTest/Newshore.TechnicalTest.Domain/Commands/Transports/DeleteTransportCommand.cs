using MediatR;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.TechnicalTest.Domain.Commands.Transports
{
    public class DeleteTransportCommand : IRequest
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FlightCarrier { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FlightNumber { get; set; } = string.Empty;
    }

    public class DeleteTransportCommandHandler : IRequestHandler<DeleteTransportCommand>
    {
        #region Properties
        private readonly ITransportCommandsRepository _repository;
        #endregion

        #region Constructor
        public DeleteTransportCommandHandler(ITransportCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(DeleteTransportCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Delete Transport] -- Start --> Transport Info: {@TransportInfo}", request);

            try
            {
                Transport transportInfo = new()
                {
                    Id = request.Id,
                    FlightCarrier = request.FlightCarrier,
                    FlightNumber = request.FlightNumber
                };

                bool result = await _repository.Delete(transportInfo);
                Log.Information("[DOMAIN Delete Transport] -- Success: {@result} --> Transport Info: {@TransportInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Delete Transport] -- Error --> Transport Info: {@TransportInfo}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
