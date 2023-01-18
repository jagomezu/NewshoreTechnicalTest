using MediatR;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.TechnicalTest.Domain.Commands.Flights
{
    public class UpdateFlightCommand : IRequest
    {
        public int Id { get; set; }

        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;

        public double Price { get; set; }

        public int TransportId { get; set; }
    }

    public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand>
    {
        #region Properties
        private readonly IFlightCommandsRepository _repository;
        #endregion

        #region Constructor
        public UpdateFlightCommandHandler(IFlightCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Update Flight] -- Start --> Flight Info: {@FlightInfo}", request);

            try
            {
                Flight transportInfo = new()
                {
                    Id = request.Id,
                    Destination = request.Destination,
                    Origin = request.Origin,
                    Price = request.Price,
                    TransportId = request.TransportId
                };

                bool result = await _repository.Update(transportInfo);
                Log.Information("[DOMAIN Update Flight] -- Success: {@result} --> Flight Info: {@FlightInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Update Flight] -- Error --> Flight Info: {@FlightInfo}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
