using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newshore.TechnicalTest.Domain.Commands.Journeys
{
    public class CreateJourneyCommand : IRequest<JourneyResponse>
    {
        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;

        public double Price { get; set; }
        
        public bool? IsDirectFlight { get; set; }

        public bool? IsRoundTripFlight { get; set; }
    }

    public class CreateJourneyCommandHandler : IRequestHandler<CreateJourneyCommand, JourneyResponse?>
    {
        #region Properties
        private readonly IJourneyCommandsRepository _repository;
        #endregion

        #region Constructor
        public CreateJourneyCommandHandler(IJourneyCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<JourneyResponse?> Handle(CreateJourneyCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Create Journey] -- Start --> Journey Info: {@JourneyInfo}", request);
            JourneyResponse result = null;

            try
            {
                Journey newJourney = new()
                {
                    Destination = request.Destination,
                    Origin = request.Origin,
                    Price = request.Price,
                    IsDirectFlight = request.IsDirectFlight,
                    IsRoundTripFlight = request.IsRoundTripFlight
                };

                Journey journeyInfo = await _repository.Create(newJourney);
                Log.Information("[DOMAIN Create Journey] -- Success --> Journey Info: {@JourneyInfo}", request);

                if (journeyInfo != null)
                {
                    result = new()
                    {
                        Id = journeyInfo.Id,
                        Destination = journeyInfo.Destination,
                        Origin = journeyInfo.Origin,
                        Price = journeyInfo.Price,
                        IsDirectFlight = journeyInfo.IsDirectFlight,
                        IsRoundTripFlight = journeyInfo.IsRoundTripFlight
                    };
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Create Journey] -- Error --> Journey Info: {@Journeynfo}", ex, request);
                throw;
            }

            return result;
        }
        #endregion
    }
}
