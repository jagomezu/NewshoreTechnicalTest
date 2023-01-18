using MediatR;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Commands.JourneyFlights
{
    public class CreateJourneyFlightCommand : IRequest
    {
        public int JourneyId { get; set; }

        public int FlightId { get; set; }
    }

    public class CreateJourneyFlightCommandHandler : IRequestHandler<CreateJourneyFlightCommand>
    {
        #region Properties
        private readonly IJourneyFlightCommandsRepository _repository;
        #endregion

        #region Constructor
        public CreateJourneyFlightCommandHandler(IJourneyFlightCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(CreateJourneyFlightCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Create Journey flight] -- Start --> Journey flight Info: {@JourneyFlightInfo}", request);

            try
            {
                JourneyFlight newJourneyFlight = new()
                {
                    JourneyId = request.JourneyId,
                    FlightId = request.FlightId
                };

                JourneyFlight result = await _repository.Create(newJourneyFlight);
                Log.Information("[DOMAIN Create Journey flight] -- Success --> Journey Flight Info: {@JourneyFlightInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Create Journey flight] -- Error --> Journey Flight Info: {@JourneyFlightInfo}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
