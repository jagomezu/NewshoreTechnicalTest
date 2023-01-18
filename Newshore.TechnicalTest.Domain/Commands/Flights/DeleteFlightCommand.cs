using MediatR;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.TechnicalTest.Domain.Commands.Flights
{
    public class DeleteFlightCommand : IRequest
    {
        public int Id { get; set; }

        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;

        public double Price { get; set; }

        public int TransportId { get; set; }
    }

    public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand>
    {
        #region Properties
        private readonly IFlightCommandsRepository _repository;
        #endregion

        #region Constructor
        public DeleteFlightCommandHandler(IFlightCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Delete Flight] -- Start --> Flight Info: {@FlightInfo}", request);

            try
            {
                Flight transportInfo = new()
                {
                    Id = request.Id,
                    Destination= request.Destination,
                    Origin = request.Origin,
                    Price = request.Price,
                    TransportId= request.TransportId
                };

                bool result = await _repository.Delete(transportInfo);
                Log.Information("[DOMAIN Delete Flight] -- Success: {@result} --> Flight Info: {@FlightInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Delete Flight] -- Error --> Flight Info: {@FlightInfo}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
