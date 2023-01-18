using MediatR;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Commands.JourneyFlights
{
    public class DeleteJourneyFlightCommand : IRequest
    {
        public int Id { get; set; }

        public int JourneyId { get; set; }

        public int FlightId { get; set; }
    }

    public class DeleteJourneyFlightCommandHandler : IRequestHandler<DeleteJourneyFlightCommand>
    {
        #region Properties
        private readonly IJourneyFlightCommandsRepository _repository;
        #endregion

        #region Constructor
        public DeleteJourneyFlightCommandHandler(IJourneyFlightCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(DeleteJourneyFlightCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Delete Journey Flight] -- Start --> Journey Flight Info: {@JourneyFlightInfo}", request);

            try
            {
                JourneyFlight journeyFlightInfo = new()
                {
                    Id = request.Id,
                    JourneyId = request.JourneyId,
                    FlightId = request.FlightId
                };

                bool result = await _repository.Delete(journeyFlightInfo);
                Log.Information("[DOMAIN Delete Journey Flight] -- Success: {@result} --> Journey Flight Info: {@JourneyFlightInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Delete Journey Flight] -- Error --> Journey Flight Info: {@JourneyFlightInfo}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
