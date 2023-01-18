using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.TechnicalTest.Domain.Commands.Flights
{
    public class CreateFlightCommand : IRequest<FlightResponse>
    {
        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;

        public double Price { get; set; }

        public int TransportId { get; set; }
    }

    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, FlightResponse?>
    {
        #region Properties
        private readonly IFlightCommandsRepository _repository;
        private readonly IFlightQueriesRepository _queriesRepository;
        #endregion

        #region Constructor
        public CreateFlightCommandHandler(IFlightCommandsRepository repository, IFlightQueriesRepository queriesRepository)
        {
            _repository = repository;
            _queriesRepository = queriesRepository;
        }
        #endregion

        #region Public methods
        public async Task<FlightResponse?> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Create Flight] -- Start --> Flight Info: {@FlightInfo}", request);
            FlightResponse? result = null;

            try
            {
                Flight newFlight = new()
                {
                    Destination = request.Destination,
                    Origin = request.Origin,
                    Price = request.Price,
                    TransportId = request.TransportId
                };

                Flight? flightInfo = await _queriesRepository.GetExistsFlight(newFlight);

                if(flightInfo == null) 
                {
                    flightInfo = await _repository.Create(newFlight);
                }

                Log.Information("[DOMAIN Create Flight] -- Success --> Flight Info: {@FlightInfo}", request);

                if (flightInfo != null)
                {
                    result = new()
                    {
                        Id = flightInfo.Id,
                        Destination = flightInfo.Destination,
                        Origin = flightInfo.Origin,
                        Price = flightInfo.Price,
                        TransportId = flightInfo.TransportId
                    };
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Create Flight] -- Error --> Flight Info: {@Flightnfo}", ex, request);
                throw;
            }

            return result;
        }
        #endregion
    }
}
