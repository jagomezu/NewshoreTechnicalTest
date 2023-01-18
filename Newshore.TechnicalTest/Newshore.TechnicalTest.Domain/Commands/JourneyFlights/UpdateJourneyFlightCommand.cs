using MediatR;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Commands.JourneyFlights
{
    public class UpdateJourneyFlightCommand : IRequest
    {
        public int Id { get; set; }

        public int JourneyId { get; set; }

        public int FlightId { get; set; }
    }

    public class UpdateJourneyFlightCommandHandler : IRequestHandler<UpdateJourneyFlightCommand>
    {
        #region Properties
        private readonly IJourneyFlightCommandsRepository _repository;
        #endregion

        #region Constructor
        public UpdateJourneyFlightCommandHandler(IJourneyFlightCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(UpdateJourneyFlightCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Update Journey Flight] -- Start --> Journey Flight Info: {@JourneyFlightInfo}", request);

            try
            {
                JourneyFlight journeyFlightInfo = new()
                {
                    Id = request.Id,
                    JourneyId = request.JourneyId,
                    FlightId = request.FlightId
                };

                bool result = await _repository.Update(journeyFlightInfo);
                Log.Information("[DOMAIN Update Journey Flight] -- Success: {@result} --> Journey Flight Info: {@JourneyFlightInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Update Journey Flight] -- Error --> Journey Flight Info: {@JourneyFlightInfo}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
