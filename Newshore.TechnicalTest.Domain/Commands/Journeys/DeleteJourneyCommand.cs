using MediatR;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.TechnicalTest.Domain.Commands.Journeys
{
    public class DeleteJourneyCommand : IRequest
    {
        public int Id { get; set; }

        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;

        public bool? IsDirectFlight { get; set; }

        public bool? IsRoundTripFlight { get; set; }

        public double Price { get; set; }
    }

    public class DeleteJourneyCommandHandler : IRequestHandler<DeleteJourneyCommand>
    {
        #region Properties
        private readonly IJourneyCommandsRepository _repository;
        #endregion

        #region Constructor
        public DeleteJourneyCommandHandler(IJourneyCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(DeleteJourneyCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Delete Journey] -- Start --> Journey Info: {@JourneyInfo}", request);

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

                bool result = await _repository.Delete(transportInfo);
                Log.Information("[DOMAIN Delete Journey] -- Success: {@result} --> Journey Info: {@JourneyInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Delete Journey] -- Error --> Journey Info: {@JourneyInfo}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
