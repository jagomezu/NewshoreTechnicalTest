using MediatR;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.TechnicalTest.Domain.Commands.Transports
{
    public class UpdateTransportCommand : IRequest
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FlightCarrier { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FlightNumber { get; set; } = string.Empty;
    }

    public class UpdateTransportCommandHandler : IRequestHandler<UpdateTransportCommand>
    {
        #region Properties
        private readonly ITransportCommandsRepository _repository;
        #endregion

        #region Constructor
        public UpdateTransportCommandHandler(ITransportCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(UpdateTransportCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Update Transport] -- Start --> Transport Info: {@TransportInfo}", request);

            try
            {
                Transport transportInfo = new()
                {
                    Id = request.Id,
                    FlightCarrier = request.FlightCarrier,
                    FlightNumber = request.FlightNumber
                };

                bool result = await _repository.Update(transportInfo);
                Log.Information("[DOMAIN Update Transport] -- Success: {@result} --> Transport Info: {@TransportInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[COMAIN Update Transport] -- Error --> Transport Info: {@TransportInfo}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
