using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Journeys
{
    public class GetJourneyListByPlacesQuery : IRequest<List<JourneyResponse>?>
    {
        public string Origin { get; set; } = string.Empty;

        public string Destination { get; set; } = string.Empty;
    }

    public class GetJourneyListByPlacesQueryHandler : IRequestHandler<GetJourneyListByPlacesQuery, List<JourneyResponse>?>
    {
        #region Properties
        private readonly IJourneyQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetJourneyListByPlacesQueryHandler(IJourneyQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<List<JourneyResponse>?> Handle(GetJourneyListByPlacesQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"[DOMAIN Get journeys by places] -- Start --> Origin: {request.Origin}, Destination: {request.Destination}");
            List<JourneyResponse>? result = null;

            try
            {
                List<Journey>? journeyList = await _repository.GetListByPlaces(request.Origin, request.Destination);

                if (journeyList != null && journeyList.Any())
                {
                    result = new();

                    journeyList.ForEach(journeyInfo =>
                    {
                        result.Add(new JourneyResponse()
                        {
                            Destination = journeyInfo.Destination,
                            Id = journeyInfo.Id,
                            Origin = journeyInfo.Origin,
                            Price = journeyInfo.Price,
                            IsDirectFlight = journeyInfo.IsDirectFlight,
                            IsRoundTripFlight = journeyInfo.IsRoundTripFlight
                        });
                    });

                    Log.Information($"[DOMAIN Get journeys by places] -- Success --> Origin: {request.Origin}, Destination: {request.Destination} -- Journey founds");
                }
                else
                {
                    Log.Warning($"[DOMAIN Get journeys by places] -- Success --> Origin: {request.Origin}, Destination: {request.Destination} -- Journeys not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get journeys by places] -- Error --> Origin: {Origin}, Destination: {Destination}", ex, request.Origin, request.Destination);

                throw;
            }

            return result;
        }
        #endregion
    }

}
