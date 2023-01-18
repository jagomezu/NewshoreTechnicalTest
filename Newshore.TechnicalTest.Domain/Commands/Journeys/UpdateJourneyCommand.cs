using MediatR;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.TechnicalTest.Domain.Commands.Journeys
{
    public class UpdateJourneyCommand : IRequest
    {
        public int Id { get; set; }

        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;

        public double Price { get; set; }

        public bool? IsDirectFlight { get; set; }

        public bool? IsRoundTripFlight { get; set; }
    }

    public class UpdateJourneyCommandHandler : IRequestHandler<UpdateJourneyCommand>
    {
        #region Properties
        private readonly IJourneyCommandsRepository _repository;
        #endregion

        #region Constructor
        public UpdateJourneyCommandHandler(IJourneyCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(UpdateJourneyCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Update Journey] -- Start --> Journey Info: {@JourneyInfo}", request);

            try
            {
                Journey transportInfo = new()
                {
                    Id = request.Id,
                    Destination = request.Destination,
                    Origin = request.Origin,
                    Price = request.Price,
                    IsDirectFlight = request.IsDirectFlight,
                    IsRoundTripFlight = request.IsRoundTripFlight
                };

                bool result = await _repository.Update(transportInfo);
                Log.Information("[DOMAIN Update Journey] -- Success: {@result} --> Journey Info: {@JourneyInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Update Journey] -- Error --> Journey Info: {@JourneyInfo}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
